
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(long id, [FromBody] Cuenta cuenta)
        {
            var items = await _service.ListarAsync();
            var existente = items.FirstOrDefault(x => x.Id == id);
            if (existente == null) return NotFound();

            // actualizar propiedades
            existente.Numero = cuenta.Numero;
            existente.Tipo = cuenta.Tipo;
            existente.SaldoInicial = cuenta.SaldoInicial;
            existente.Estado = cuenta.Estado;
            existente.ClienteId = cuenta.ClienteId;

            var actualizado = await _service.ActualizarAsync(existente);
            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var eliminado = await _service.EliminarAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
