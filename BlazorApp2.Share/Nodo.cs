using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{

    public class Nodo
    {
        public Nodo ReferenciaIzquierda { get; set; }
        public Nodo ReferenciaDerecha { get; set; }
        public Nodo ReferenciaArriba { get; set; }
        public Nodo ReferenciaAbajo { get; set; }
        public object Informacion { get; set; }
        public bool TieneSemaforo { get; set; }

        // public Semaforo semaforo { get; set; } por implementar
        public int ConteoHistoricoVehiculos { get; set; }
        public int ConteoActualVehiculos { get; set; }
        public bool Visitado { get; set; } = false; 


        public Nodo()
        {
            ReferenciaIzquierda = null;
            ReferenciaDerecha = null;
            ReferenciaArriba = null;
            ReferenciaAbajo = null;
            Informacion = null;
            TieneSemaforo = false;
            ConteoHistoricoVehiculos = 0;
            ConteoActualVehiculos = 0;
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


    }
}

