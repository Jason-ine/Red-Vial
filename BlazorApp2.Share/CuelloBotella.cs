using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class CuelloBotella
    {
        [Key] 
        public int NodoId { get; set; }
        public string DireccionSemaforo { get; set; }
        public int TotalCambios { get; set; }
        public int SumaCantidadEspera { get; set; }
        public double IndicadorCongestion { get; set; }
        public CuelloBotella() { }
    }
}
