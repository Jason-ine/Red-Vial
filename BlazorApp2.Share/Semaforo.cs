using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class Semaforo
    {
        public bool EstadoVertical { get; private set; } = true;
        public bool EstadoHorizontal { get; private set; } = false;

        public int TiempoVerdeVertical { get; set; } = 5;
        public int TiempoVerdeHorizontal { get; set; } = 5;

        private int contador = 0;
        private static Random random = new Random();

        public void AvanzarTiempo()
        {
            contador++;

            if (EstadoVertical && contador >= TiempoVerdeVertical)
            {
                CambiarEstado();
            }
            else if (EstadoHorizontal && contador >= TiempoVerdeHorizontal)
            {
                CambiarEstado();
            }
        }
        public void CambiarEstado()
        {
            EstadoVertical = !EstadoVertical;
            EstadoHorizontal = !EstadoHorizontal;
            contador = 0;
        }

        public string ObtenerEstadoTexto()
        {
            if (EstadoVertical)
                return "Verde (Vertical) / Rojo (Horizontal)";
            else
                return "Rojo (Vertical) / Verde (Horizontal)";
        }

        public void ActualizarDetalle(DetalleSemaforo detalle)
        {
            if (detalle == null)
                return;

            int vehiculosEsperando = random.Next(5, 21);
            int vehiculosPasaron = random.Next(1, vehiculosEsperando + 1);

            int tiempoRojo = EstadoVertical ? TiempoVerdeHorizontal : TiempoVerdeVertical;
            int tiempoVerde = EstadoVertical ? TiempoVerdeVertical : TiempoVerdeHorizontal;

            detalle.RegistrarCambioEstado(vehiculosEsperando, tiempoRojo, vehiculosPasaron, tiempoVerde);
            detalle.FinalizarRegistro();
        }
    }
}
