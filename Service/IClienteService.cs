
using ProyectoApp.Model;

namespace ProyectoApp.Service
{
    public interface IClienteService
    {
        Task<Cliente> CrearAsync(Cliente cliente);
        Task<List<Cliente>> ListarAsync();
    }
}
