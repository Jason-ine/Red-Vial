using iText.Commons.Bouncycastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class InterseccionCongestionadaContainer : BaseContainer
    {
        public new InterseccionCongestionada? Item1 { get; set; }
        public new InterseccionCongestionada? Item2 { get; set; }
        public new InterseccionCongestionada? Item3 { get; set; }
        public InterseccionCongestionada? GetItem(int index)
        {
            return index switch
            {
                0 => Item1,
                1 => Item2,
                2 => Item3,
                _ => null
            };
        }
    }
}
