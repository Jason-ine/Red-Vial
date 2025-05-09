using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    
        public class InterseccionCongestionada
    {
            [Key]
            public int NodoId { get; set; }
            public int TotalVehiculos { get; set; }
            public int TotalCambios { get; set; }
            public double IndicadorCongestion { get; set; }
            public InterseccionCongestionada(){}

          }

}
