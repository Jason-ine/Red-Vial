using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class DetalleSemaforoContainer
    {
        public DetalleSemaforo? Item1 { get; set; }
        public DetalleSemaforo? Item2 { get; set; }
        public DetalleSemaforo? Item3 { get; set; }
        public DetalleSemaforo? Item4 { get; set; }
        public DetalleSemaforo? Item5 { get; set; }
        public DetalleSemaforo? Item6 { get; set; }
        public DetalleSemaforo? Item7 { get; set; }
        public DetalleSemaforo? Item8 { get; set; }
        public DetalleSemaforo? Item9 { get; set; }
        public DetalleSemaforo? Item10 { get; set; }
        public int TotalItems { get; set; }

        public DetalleSemaforo? GetItem(int index)
        {
            return index switch
            {
                0 => Item1,
                1 => Item2,
                2 => Item3,
                3 => Item4,
                4 => Item5,
                5 => Item6,
                6 => Item7,
                7 => Item8,
                8 => Item9,
                9 => Item10,
                _ => null
            };
        }
    }

}
