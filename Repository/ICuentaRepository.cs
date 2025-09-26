
using ProyectoApp.Model;

namespace ProyectoApp.Repository
{
    public interface ICuentaRepository
    {
        Task<Cuenta?> FindByNumeroAsync(string numero);
        Task<List<Cuenta>> FindAllAsync();
        Task AddAsync(Cuenta cuenta);
    }
}
