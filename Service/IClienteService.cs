using ProyectoApp.Model;

namespace ProyectoApp.Service
{
    public interface IClienteService
    {
        Task<Cliente> CrearAsync(Cliente cliente);
        Task<List<Cliente>> ListarAsync();
        Task<Cliente?> ObtenerPorIdAsync(long id);
        Task<Cliente?> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(long id);
    }
}
