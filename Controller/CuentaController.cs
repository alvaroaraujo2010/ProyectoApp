
using Microsoft.AspNetCore.Mvc;
using ProyectoApp.Model;
using ProyectoApp.Service;

namespace ProyectoApp.Controller
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _service;
        public CuentaController(ICuentaService service) { _service = service; }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Cuenta cuenta)
        {
            var c = await _service.CrearAsync(cuenta);
            return CreatedAtAction(nameof(Obtener), new { numero = c.Numero }, c);
        }

        [HttpGet]
        public async Task<IActionResult> Listar() => Ok(await _service.ListarAsync());

        [HttpGet("{numero}")]
        public async Task<IActionResult> Obtener(string numero)
        {
            var c = await _service.ObtenerPorNumeroAsync(numero);
            if (c==null) return NotFound();
            return Ok(c);
        }
    }
}
