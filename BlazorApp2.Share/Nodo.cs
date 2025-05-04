using System;
using System.Collections.Generic;
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
        }
    

}
