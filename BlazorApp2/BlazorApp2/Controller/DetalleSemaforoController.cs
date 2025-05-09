using BlazorApp2.Data;
using BlazorApp2.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BlazorApp2.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleSemaforoController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public DetalleSemaforoController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("ConexionServidor")]
        public async Task<ActionResult<string>> GetEjemplo()
        {
            return "conectado con el servidor";
        }
        [HttpGet("ConexionDetalleSemaforoDetalle")]
        public async Task<ActionResult<string>> GetConexionDetalleSemaforoDetalle()
        {
            try
            {
                var respuesta = await _context.DetalleSemaforo.ToListAsync();
                return "conectado con la tabla DetalleSemaforo";
            }
            catch(Exception ex)
            {
                return "error de conexion con la tabla DetalleSemaforo";
            }
        }
        
        [HttpPost("GuardarSemaforo")]
        public async Task<IActionResult> GuardarSemaforo([FromBody] DetalleSemaforo semaforo)
        {

            
            Console.WriteLine($"Datos recibidos: {JsonSerializer.Serialize(semaforo)}");
            _context.DetalleSemaforo.Add(semaforo);
            await _context.SaveChangesAsync();
            return Ok("Semáforo guardado");
        }

        [HttpGet("ObtenerDetalle")]
        public async Task<ActionResult<DetalleSemaforoContainer>> GetDetalleSemaforo(
            [FromQuery]int pagina = 1)
        {
            var query = _context.DetalleSemaforo.AsQueryable();
            var totalItems = await query.CountAsync();
            var contenedor = new DetalleSemaforoContainer
            {
                TotalItems = totalItems,
                Item1 = await query.Skip((pagina - 1) * 10 + 0).FirstOrDefaultAsync(),
                Item2 = await query.Skip((pagina - 1) * 10 + 1).FirstOrDefaultAsync(),
                Item3 = await query.Skip((pagina - 1) * 10 + 2).FirstOrDefaultAsync(),
                Item4 = await query.Skip((pagina - 1) * 10 + 3).FirstOrDefaultAsync(),
                Item5 = await query.Skip((pagina - 1) * 10 + 4).FirstOrDefaultAsync(),
                Item6 = await query.Skip((pagina - 1) * 10 + 5).FirstOrDefaultAsync(),
                Item7 = await query.Skip((pagina - 1) * 10 + 6).FirstOrDefaultAsync(),
                Item8 = await query.Skip((pagina - 1) * 10 + 7).FirstOrDefaultAsync(),
                Item9 = await query.Skip((pagina - 1) * 10 + 8).FirstOrDefaultAsync(),
                Item10 = await query.Skip((pagina - 1) * 10 + 9).FirstOrDefaultAsync()
            };

            return Ok(contenedor);
        }

        [HttpGet("ObtenerFiltradoCombinado")]
        public async Task<ActionResult<SemaforoEstadisticaContainer>> GetFiltradoCombinado(
        [FromQuery] int? nodoId = null,
        [FromQuery] string? direccion = null,
        [FromQuery] int pagina = 1)
        {
            var query = _context.SemaforoEstadisticas.AsQueryable();

            if (nodoId.HasValue)
                query = query.Where(d => d.NodoId == nodoId.Value);

            if (!string.IsNullOrWhiteSpace(direccion))
                query = query.Where(d => EF.Functions.Like(d.DireccionSemaforo, $"%{direccion}%"));

            var totalItems = await query.CountAsync();
            var contenedor = new SemaforoEstadisticaContainer
            {
                TotalItems = totalItems,
                Item1 = await query.Skip((pagina - 1) * 10 + 0).FirstOrDefaultAsync(),
                Item2 = await query.Skip((pagina - 1) * 10 + 1).FirstOrDefaultAsync(),
                Item3 = await query.Skip((pagina - 1) * 10 + 2).FirstOrDefaultAsync(),
                Item4 = await query.Skip((pagina - 1) * 10 + 3).FirstOrDefaultAsync(),
                Item5 = await query.Skip((pagina - 1) * 10 + 4).FirstOrDefaultAsync(),
                Item6 = await query.Skip((pagina - 1) * 10 + 5).FirstOrDefaultAsync(),
                Item7 = await query.Skip((pagina - 1) * 10 + 6).FirstOrDefaultAsync(),
                Item8 = await query.Skip((pagina - 1) * 10 + 7).FirstOrDefaultAsync(),
                Item9 = await query.Skip((pagina - 1) * 10 + 8).FirstOrDefaultAsync(),
                Item10 = await query.Skip((pagina - 1) * 10 + 9).FirstOrDefaultAsync()
            };

            return Ok(contenedor);
        }
        [HttpDelete("BorrarTodos")]
        public async Task<IActionResult> BorrarTodosRegistros()
        {
            try
            {
                int registrosBorrados = 0;
                bool hayMasRegistrosDetalleSemaforo = true;
                bool hayMasRegistrosSemaforoEstadistica = true;

                while (hayMasRegistrosDetalleSemaforo)
                {
                    var registroDetalle = await _context.DetalleSemaforo.FirstOrDefaultAsync();

                    if (registroDetalle == null)
                    {
                        hayMasRegistrosDetalleSemaforo = false;
                        continue;
                    }

                    _context.DetalleSemaforo.Remove(registroDetalle);
                    registrosBorrados += await _context.SaveChangesAsync();
                }

                while (hayMasRegistrosSemaforoEstadistica)
                {
                    var registroEstadistica = await _context.SemaforoEstadisticas.FirstOrDefaultAsync();

                    if (registroEstadistica == null)
                    {
                        hayMasRegistrosSemaforoEstadistica = false;
                        continue;
                    }

                    _context.SemaforoEstadisticas.Remove(registroEstadistica);
                    registrosBorrados += await _context.SaveChangesAsync();
                }

                return Ok($"Se borraron {registrosBorrados} registros correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al borrar registros: {ex.Message}");
            }
        }

        [HttpGet("InterseccionesMasCongestionadas")]
        public async Task<ActionResult<InterseccionCongestionadaContainer>> GetInterseccionesMasCongestionadas()
        {
            var result = new InterseccionCongestionadaContainer();

            var data = await _context.InterseccionesCongestionadas
                .FromSqlRaw("EXEC InterseccionesMasCongestionadas")
                .ToListAsync();

            if (data.Count > 0) result.Item1 = data[0];
            if (data.Count > 1) result.Item2 = data[1];
            if (data.Count > 2) result.Item3 = data[2];

            result.TotalItems = data.Count;

            return Ok(result);
        }

        [HttpGet("AnalisisCuelloBotella")]
        public async Task<ActionResult<CuelloBotellaContainer>> GetAnalisisCuelloBotella()
        {
            var result = new CuelloBotellaContainer();

            var data = await _context.CuellosBotella
                .FromSqlRaw("EXEC AnalisisCuelloBotella")
                .ToListAsync();

            if (data.Count > 0) result.Item1 = data[0];
            if (data.Count > 1) result.Item2 = data[1];
            if (data.Count > 2) result.Item3 = data[2];
            if (data.Count > 3) result.Item4 = data[3];
            if (data.Count > 4) result.Item5 = data[4];
            if (data.Count > 5) result.Item6 = data[5];
            if (data.Count > 6) result.Item7 = data[6];
            if (data.Count > 7) result.Item8 = data[7];
            if (data.Count > 8) result.Item9 = data[8];
            if (data.Count > 9) result.Item10 = data[9];

            result.TotalItems = data.Count;

            return Ok(result);
        }
        [HttpPost("EjecutarEstadisticas")]
        public async Task<IActionResult> EjecutarEstadisticas()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC sp_InsertarOActualizarEstadisticas");
                return Ok(new { mensaje = "Estadísticas procesadas correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }



    }
}

