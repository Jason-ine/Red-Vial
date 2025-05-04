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
        public double TiempoPromedioPorCarro { get; set; } = 0;
        public int totalCambios { get; set; } = 0;

        private int sumaTiempoRojo;
        private int sumaTiempoVerde;
        private int sumaVehiculosPasaron;

        public DetalleSemaforo() { }

        public DetalleSemaforo(int nodoId, string direccion)
        {
            NodoId = nodoId;
            DireccionSemaforo = direccion;
        }

        public void RegistrarCambioEstado(int cantidadVehiculosEsperando, int tiempoRojo, int vehiculosPasaron, int tiempoVerde)
        {
            if (cantidadVehiculosEsperando <= 0 || tiempoRojo <= 0 || vehiculosPasaron <= 0 || tiempoVerde <= 0)
                return;

            double tiempoEsperaPromedio = tiempoRojo / 2.0;
            double tiempoCrucePromedio = (double)tiempoVerde / vehiculosPasaron;
            double tiempoPromedioPorCarroCambio = tiempoEsperaPromedio + tiempoCrucePromedio;

            SumaCantidadEspera += cantidadVehiculosEsperando;
            sumaTiempoRojo += tiempoRojo;
            sumaTiempoVerde += tiempoVerde;
            sumaVehiculosPasaron += vehiculosPasaron;
            totalCambios++;
        }

        public void FinalizarRegistro()
        {
            if (totalCambios == 0) return;

            PromedioVehiculosPorCambio = (double)sumaVehiculosPasaron / totalCambios;
            double tiempoEsperaPromedio = (sumaTiempoRojo / (double)totalCambios) / 2.0;
            double tiempoCrucePromedio = (double)sumaTiempoVerde / sumaVehiculosPasaron;
            TiempoPromedioPorCarro = tiempoEsperaPromedio + tiempoCrucePromedio;
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