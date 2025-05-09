using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlazorApp2.Share
{
    public class DetalleSemaforo
    {
        public int Id { get; set; }
        public int NodoId { get; set; }
        public string DireccionSemaforo { get; set; }
        public int SumaCantidadEspera { get; set; } = 0;
        public double PromedioVehiculosPorCambio { get; set; } = 0;
        public double TiempoCrucePromedio { get; set; } = 0;
        public double TiempoPromedioPorCarro { get; set; } = 0;
        public int totalCambios { get; set; } = 0;

        private int sumaTiempoRojo = 0;
        private int sumaTiempoVerde=0;
        private int sumaVehiculosPasaron =0;
        private int cantidadVehiculosEsperando1 = 0;

        public DetalleSemaforo() { }

        public DetalleSemaforo(int nodoId, string direccion)
        {
            NodoId = nodoId;
            DireccionSemaforo = direccion;
        }

        public void RegistrarCambioEstado(int cantidadVehiculosEsperando, int tiempoRojo, int vehiculosPasaron, int tiempoVerde, bool huboCambio)
        {
            if (huboCambio == false)
            {
                double tiempoEsperaPromedio = tiempoRojo / 2.0;
                double tiempoCrucePromedio = (double)tiempoVerde / vehiculosPasaron;
                double tiempoPromedioPorCarroCambio = tiempoEsperaPromedio + tiempoCrucePromedio;
                if (cantidadVehiculosEsperando % 2 != 0)
                {
                    cantidadVehiculosEsperando += 1;
                     cantidadVehiculosEsperando1 = cantidadVehiculosEsperando / 2;
                }
                else
                {
                    cantidadVehiculosEsperando1 = cantidadVehiculosEsperando / 2;
                }

                SumaCantidadEspera += cantidadVehiculosEsperando1;
                sumaTiempoRojo += tiempoRojo;
                sumaTiempoVerde += tiempoVerde;
                sumaVehiculosPasaron += vehiculosPasaron;

            }
            else if(huboCambio == true)
            {
                double tiempoEsperaPromedio = tiempoRojo / 2.0;
                double tiempoCrucePromedio = (double)tiempoVerde / vehiculosPasaron;
                double tiempoPromedioPorCarroCambio = tiempoEsperaPromedio + tiempoCrucePromedio;
                cantidadVehiculosEsperando1 = cantidadVehiculosEsperando / 2;
                if (cantidadVehiculosEsperando % 2 != 0)
                {
                    cantidadVehiculosEsperando += 1;
                    cantidadVehiculosEsperando1 = cantidadVehiculosEsperando / 2;
                }
                else
                {
                    cantidadVehiculosEsperando1 = cantidadVehiculosEsperando / 2;
                }
                SumaCantidadEspera += cantidadVehiculosEsperando1;
                sumaTiempoRojo += tiempoRojo;
                sumaTiempoVerde += tiempoVerde;
                sumaVehiculosPasaron += vehiculosPasaron;
                totalCambios++;
            }

        }

        public void FinalizarRegistro()
        {
            if (totalCambios == 0 || sumaVehiculosPasaron == 0)
            {
                PromedioVehiculosPorCambio = 0;
                TiempoCrucePromedio = 0;
                TiempoPromedioPorCarro = 0;
                return;
            }

            PromedioVehiculosPorCambio = (double)sumaVehiculosPasaron / totalCambios;
            double tiempoEsperaPromedio = (sumaTiempoRojo / (double)totalCambios) / 2.0;
            TiempoCrucePromedio = (double)sumaTiempoVerde / sumaVehiculosPasaron;
            TiempoPromedioPorCarro = tiempoEsperaPromedio + TiempoCrucePromedio;
        }

        public void ReiniciarAcumuladores()
        {
            SumaCantidadEspera = 0;
            sumaTiempoRojo = 0;
            sumaTiempoVerde = 0;
            sumaVehiculosPasaron = 0;
            totalCambios = 0;
            PromedioVehiculosPorCambio = 0;
            TiempoPromedioPorCarro = 0;
        }

        public void ReiniciarTodo()
        {
            ReiniciarAcumuladores();
            Id = 0;
            NodoId = 0;
            DireccionSemaforo = null;
        }
    }
}