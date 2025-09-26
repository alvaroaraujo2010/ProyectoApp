
using ProyectoApp.Model;

namespace ProyectoApp.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente?> FindByIdAsync(long id);
        Task<Cliente?> FindByIdentificacionAsync(string identificacion);
        Task<List<Cliente>> FindAllAsync();
        Task AddAsync(Cliente cliente);
    }
}
