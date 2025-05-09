using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public Nodo NodoActual { get; set; } = null!;
        public string NodoAnterior { get; set; } = "";
        public bool SeMovio { get; set; } = false;
        public string? DireccionPendiente { get; set; }

    }
}