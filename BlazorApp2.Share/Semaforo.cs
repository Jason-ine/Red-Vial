using System;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class Semaforo
    {
        public int Id { get; set; }
        public static bool EstadoVertical { get; set; } = true;
        public static bool EstadoHorizontal { get; set; } = false;
        public static event Action? OnEstadoChanged;

        public int TiempoVerdeVertical { get; set; } = 10000; // por si en el futuro querés instancias
        public int TiempoVerdeHorizontal { get; set; } = 10000;
        public int TiempoCambio { get; set; } = 10;

        private static bool enEjecucion = false;

        public Semaforo() { }

        public Semaforo(int id, int tiempoVerdeVertical, int tiempoVerdeHorizontal)
        {
            Id = id;
            TiempoVerdeVertical = tiempoVerdeVertical;
            TiempoVerdeHorizontal = tiempoVerdeHorizontal;
        }

        public static async Task IniciarSemaforo()
        {
            if (enEjecucion) return; // evita múltiples inicios
            enEjecucion = true;

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
