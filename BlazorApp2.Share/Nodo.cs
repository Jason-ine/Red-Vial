using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{

    public class Nodo
    {
        private int _conteoVehiculos;
        public Nodo ReferenciaIzquierda { get; set; }
        public Nodo ReferenciaDerecha { get; set; }
        public Nodo ReferenciaArriba { get; set; }
        public Nodo ReferenciaAbajo { get; set; }
        public object Informacion { get; set; }
        public bool TieneSemaforo { get; set; }
        public bool viaDerecha { get; set; }
        public bool viaIzquierda { get; set; }
        public bool viaArriba { get; set; }
        public bool viaAbajo {  get; set; }
        public Semaforo semaforo { get; set; }
        public int VehiculosEnEsperaVertical { get; set; }
        public int VehiculosEnEsperaHorizontal { get; set; }
        public int TotalVehiculosPasados { get; set; }


        public int ConteoHistoricoVehiculos
        {
            get => _conteoVehiculos;
            set
            {
                if (_conteoVehiculos < 0)
                {
                    _conteoVehiculos = 0;
                }
                else
                {
                    _conteoVehiculos = value;
                }
            }
        }
        public int ConteoActualVehiculos { get; set; }
        public bool Visitado { get; set; } = false; 
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Nodo()
        {
            ReferenciaIzquierda = null;
            ReferenciaDerecha = null;
            ReferenciaArriba = null;
            ReferenciaAbajo = null;
            Informacion = null;
            TieneSemaforo = false;
            TieneSemaforo = false;
            ConteoHistoricoVehiculos = 0;
            ConteoActualVehiculos = 0;
            VehiculosEnEsperaVertical = 0;
            VehiculosEnEsperaHorizontal = 0;
            TotalVehiculosPasados = 0;
        }


        public Nodo(Nodo referenciaIzquierda, Nodo referenciaDerecha, Nodo referenciaArriba, Nodo referenciaAbajo, object informacion, bool tieneSemaforo, int conteoHistoricoVehiculos, int conteoActualVehiculos)
        {
            ReferenciaIzquierda = referenciaIzquierda;
            ReferenciaDerecha = referenciaDerecha;
            ReferenciaArriba = referenciaArriba;
            ReferenciaAbajo = referenciaAbajo;
            Informacion = informacion;
            TieneSemaforo = tieneSemaforo;
            ConteoHistoricoVehiculos = conteoHistoricoVehiculos;
            ConteoActualVehiculos = conteoActualVehiculos;

        }

        private static void LimpiarVisitados(Nodo actual)
        {
            


            if (actual == null || !actual.Visitado)
                return;

            actual.Visitado = false;

            LimpiarVisitados(actual.ReferenciaDerecha);
            LimpiarVisitados(actual.ReferenciaIzquierda);
            LimpiarVisitados(actual.ReferenciaArriba);
            LimpiarVisitados(actual.ReferenciaAbajo);
        }

        public static int Buscar(Nodo inicio, Nodo destino)
        {
            if (inicio == null || destino == null)
                return -1;

            LimpiarVisitados(inicio);

            Nodo[] cola = new Nodo[100];
            int[] pasos = new int[100];

            int frente = 0;
            int fin = 0;

            cola[fin] = inicio;
            pasos[fin] = 0;
            inicio.Visitado = true;
            fin++;

            while (frente < fin)
            {
                Nodo actual = cola[frente];
                int pasoActual = pasos[frente];
                frente++;

                if (actual == destino)
                    return pasoActual;

                EncolarSiValido(cola, pasos, ref fin, actual.ReferenciaDerecha, pasoActual + 1);
                EncolarSiValido(cola, pasos, ref fin, actual.ReferenciaIzquierda, pasoActual + 1);
                EncolarSiValido(cola, pasos, ref fin, actual.ReferenciaArriba, pasoActual + 1);
                EncolarSiValido(cola, pasos, ref fin, actual.ReferenciaAbajo, pasoActual + 1);
            }

            return -1;
        }

        private static void EncolarSiValido(Nodo[] cola, int[] pasos, ref int fin, Nodo nodo, int paso)
        {
            if (nodo != null && !nodo.Visitado)
            {
                nodo.Visitado = true;
                cola[fin] = nodo;
                pasos[fin] = paso;
                fin++;
            }
        }


        public async Task<int> SacarVehiculosDelNodo(Nodo nodoActual)
        {
            int VehiculosPasaron = 0;

            if (ConteoActualVehiculos != 0)
            {
                while(Semaforo.EstadoVertical)
                {
                    Console.WriteLine("Pasó un vehículo");
                    VehiculosPasaron++;
                    ConteoActualVehiculos--;
                    TotalVehiculosPasados++; 
                    await Task.Delay(225);
                }
            }
            else
            {
                Debug.WriteLine("No hay vehículos en el nodo");
            }

            return VehiculosPasaron;
        }

        public void LlegadaVehiculo(bool esVertical)
        {
            if (esVertical)
            {
                VehiculosEnEsperaVertical++;
            }
            else
            {
                VehiculosEnEsperaHorizontal++;
            }
            ConteoActualVehiculos++; 
        }

        public void DecrementarVehiculosEnEspera(bool esVertical, int cantidad)
        {
            if (esVertical)
            {
                VehiculosEnEsperaVertical -= cantidad;
                if (VehiculosEnEsperaVertical < 0)
                    VehiculosEnEsperaVertical = 0;
            }
            else
            {
                VehiculosEnEsperaHorizontal -= cantidad;
                if (VehiculosEnEsperaHorizontal < 0)
                    VehiculosEnEsperaHorizontal = 0;
            }
        }

    }
}

