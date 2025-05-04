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
        [HttpGet("ConexionDetalleSemaforoEsta")]
        public async Task<ActionResult<string>> GetConexionDetalleSemaforoDetalleEsta()
        {
            try
            {
                var respuesta = await _context.EstadisticaSemaforo.ToListAsync();
                return "conectado con la tabla EstadisticaSemaforo";
            }
            catch (Exception ex)
            {
                return "error de conexion con la tabla EstditticaSemaforo";
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
        public async Task<ActionResult<DetalleSemaforoContainer>> GetFiltradoCombinado(
        [FromQuery] int? nodoId = null,
        [FromQuery] string? direccion = null,
        [FromQuery] int pagina = 1)
        {
            var query = _context.DetalleSemaforo.AsQueryable();

            if (nodoId.HasValue)
                query = query.Where(d => d.NodoId == nodoId.Value);

            if (!string.IsNullOrWhiteSpace(direccion))
                query = query.Where(d => EF.Functions.Like(d.DireccionSemaforo, $"%{direccion}%"));

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
        [HttpDelete("BorrarTodos")]
        public async Task<IActionResult> BorrarTodosRegistros()
        {
            try
            {
                int registrosBorrados = 0;
                bool hayMasRegistros = true;

                while (hayMasRegistros)
                {
                    var registro = await _context.DetalleSemaforo.FirstOrDefaultAsync();

                    if (registro == null)
                    {
                        hayMasRegistros = false;
                        continue;
                    }

                    _context.DetalleSemaforo.Remove(registro);
                    registrosBorrados += await _context.SaveChangesAsync();
                }

                return Ok($"Se borraron {registrosBorrados} registros correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al borrar registros: {ex.Message}");
            }
        }

    }
}

