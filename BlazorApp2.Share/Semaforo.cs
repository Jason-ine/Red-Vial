using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class Semaforo
    {
        public int Id { get; set; }
        public static bool EstadoVertical { get; set; } = true;
        public static bool EstadoHorizontal { get; set; } = false;
        public static event Action? OnEstadoChanged;
        public int TiempoVerdeVertical { get; set; }
        public int TiempoVerdeHorizontal { get; set; }
        public int TiempoCambio { get; set; } = 10; 

        public Semaforo() { }

        public Semaforo(int id, int tiempoVerdeVertical, int tiempoVerdeHorizontal)
        {
            Id = id;
            TiempoVerdeVertical = tiempoVerdeVertical;
            TiempoVerdeHorizontal = tiempoVerdeHorizontal;
        }

        public static async Task IniciarSemaforo()
        {
            while (true)
            {
                await Task.Delay(10000); 
                CambiarEstado();
            }
        }

        public static void CambiarEstado()
        {
            EstadoVertical = !EstadoVertical;
            EstadoHorizontal = !EstadoHorizontal;
            OnEstadoChanged?.Invoke();
        }
    }
}

     