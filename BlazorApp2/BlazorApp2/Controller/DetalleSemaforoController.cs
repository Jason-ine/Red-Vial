using BlazorApp2.Data;
using BlazorApp2.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
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

        [HttpGet("SumaAyB")]
        public async Task<ActionResult<double>> GetSumaAyB(string nodos)
        {
            if (string.IsNullOrEmpty(nodos))
            {
                return BadRequest("No se recibieron nodos.");
            }

            string[] nodosArray = nodos.Split(';');
            string consultaSQL = "";
            for(int i = 0; i < nodosArray.Length; i++)
            { 
              
                consultaSQL += nodosArray[i] + ",";
                
            }
            Console.WriteLine(consultaSQL);
            consultaSQL = consultaSQL.TrimEnd(',');
            Debug.WriteLine(consultaSQL);

            if (nodos!=null && nodos.Length > 0)
            {
                double resultado = 0;
                var sqlQuery = $"SELECT SUM(SumaCantidadEspera) AS TotalVehiculos FROM SemaforoEstadisticas WHERE NodoId IN ({consultaSQL})";
                Debug.WriteLine(sqlQuery);
                var conn =  _context.Database.GetDbConnection();

                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sqlQuery;
                        var valor = cmd.ExecuteScalar();

                        if (valor != DBNull.Value)
                            resultado = Convert.ToDouble(valor);
                        Debug.WriteLine(resultado);
                    }
                }
                finally
                {
                    conn.Close();
                }
                return Ok(resultado);
            }
            else
            {
                return BadRequest("No se recibieron nodos.");
            }
        }







        [HttpGet("AnalisisCuelloBotella")]
        public async Task<ActionResult<CuelloBotellaContainer>> GetAnalisisCuelloBotella([FromQuery] int pagina = 1)
        {
            var connection = _context.Database.GetDbConnection();
            var cuelloBotellaList = new List<CuelloBotella>();

            try
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = "AnalisisCuelloBotella"; 
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    cuelloBotellaList.Add(new CuelloBotella
                    {
                        NodoId = reader.GetInt32(0),
                        DireccionSemaforo = reader.GetString(1),
                        TotalCambios = reader.GetInt32(2),
                        SumaCantidadEspera = reader.GetInt32(3),
                        IndicadorCongestion = reader.GetDouble(4)
                    });
                }
            }
            finally
            {
                await connection.CloseAsync();
            }

            var totalItems = cuelloBotellaList.Count;
            var paginados = cuelloBotellaList.Skip((pagina - 1) * 10).Take(10).ToList();

            var contenedor = new CuelloBotellaContainer
            {
                TotalItems = totalItems,
                Item1 = paginados.ElementAtOrDefault(0),
                Item2 = paginados.ElementAtOrDefault(1),
                Item3 = paginados.ElementAtOrDefault(2),
                Item4 = paginados.ElementAtOrDefault(3),
                Item5 = paginados.ElementAtOrDefault(4),
                Item6 = paginados.ElementAtOrDefault(5),
                Item7 = paginados.ElementAtOrDefault(6),
                Item8 = paginados.ElementAtOrDefault(7),
                Item9 = paginados.ElementAtOrDefault(8),
                Item10 = paginados.ElementAtOrDefault(9)
            };

            return Ok(contenedor);
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

