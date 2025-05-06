using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class CuelloBotellaContainer : BaseContainer
    {
        public new CuelloBotella? Item1 { get; set; }
        public new CuelloBotella? Item2 { get; set; }
        public new CuelloBotella? Item3 { get; set; }
        public new CuelloBotella? Item4 { get; set; }
        public new CuelloBotella? Item5 { get; set; }
        public new CuelloBotella? Item6 { get; set; }
        public new CuelloBotella? Item7 { get; set; }
        public new CuelloBotella? Item8 { get; set; }
        public new CuelloBotella? Item9 { get; set; }
        public new CuelloBotella? Item10 { get; set; }
        public CuelloBotella? GetItem(int index)
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
