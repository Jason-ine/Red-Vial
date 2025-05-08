using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class SemaforoEstadistica
    {
        public int Id { get; set; }
        public int NodoId { get; set; }
        public string DireccionSemaforo { get; set; }
        public int SumaCantidadEspera { get; set; }
        public double PromedioVehiculosPorCambio { get; set; }
        public double TiempoCrucePromedio { get; set; }
        public double TiempoPromedioPorCarro { get; set; }
        public int TotalCambios { get; set; }
        public SemaforoEstadistica()
        {
        }
    }

}
