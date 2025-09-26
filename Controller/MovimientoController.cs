using Microsoft.AspNetCore.Mvc;
using ProyectoApp.Model;
using ProyectoApp.Service;

namespace ProyectoApp.Controller
{
    [ApiController]
    [Route("api/movimientos")]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _service;
        public MovimientoController(IMovimientoService service) { _service = service; }

        [HttpPost]
        public async Task<IActionResult> Crear(
            [FromQuery] string numeroCuenta,
            [FromQuery] string tipo,
            [FromQuery] decimal valor,
            [FromQuery] string? referencia,
            [FromQuery] string tipoMovimiento
        )
        {
            try
            {
                var m = await _service.CreateAsync(
                    numeroCuenta,
                    tipo,
                    valor,
                    referencia ?? string.Empty,
                    tipoMovimiento
                );

                return CreatedAtAction(nameof(Obtener), new { id = m.Id }, m);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("cuenta/{numero}")]
        public async Task<IActionResult> ListarPorCuenta(string numero)
        {
            try
            {
                var from = DateTime.UtcNow.AddYears(-100);
                var to = DateTime.UtcNow;
                var list = await _service.ListByCuentaAsync(numero, from, to);
                return Ok(list);
            }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }

        [HttpGet("all")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var list = await _service.ListAllWithSaldoAsync();
                return Ok(list);
            }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }

        [HttpGet("reporte")]
        public async Task<IActionResult> Reporte([FromQuery] string numeroCuenta)
        {
            try
            {
                var from = DateTime.UtcNow.AddYears(-100);
                var to = DateTime.UtcNow;
                var rep = await _service.GenerarReporteAsync(numeroCuenta, from, to);
                return Ok(rep);
            }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(long id)
        {
            // Solo placeholder
            return Ok(new { mensaje = $"Movimiento con ID {id}" });
        }
    }
}
