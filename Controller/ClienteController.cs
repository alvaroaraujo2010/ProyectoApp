
using Microsoft.AspNetCore.Mvc;
using ProyectoApp.Model;
using ProyectoApp.Service;

namespace ProyectoApp.Controller
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;
        public ClienteController(IClienteService service) { _service = service; }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Cliente cliente)
        {
            var c = await _service.CrearAsync(cliente);
            return CreatedAtAction(nameof(Obtener), new { id = c.Id }, c);
        }

        [HttpGet]
        public async Task<IActionResult> Listar() => Ok(await _service.ListarAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(long id)
        {
            var items = await _service.ListarAsync();
            var item = items.FirstOrDefault(x=>x.Id==id);
            if (item==null) return NotFound();
            return Ok(item);
        }
    }
}
