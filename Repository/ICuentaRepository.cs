
using ProyectoApp.Model;

namespace ProyectoApp.Repository
{
    public interface ICuentaRepository
    {
        Task<Cuenta?> FindByNumeroAsync(string numero);
        Task<Cuenta?> FindByIdAsync(long id);
        Task<List<Cuenta>> FindAllAsync();
        Task AddAsync(Cuenta cuenta);
        Task ActualizarAsync(Cuenta cuenta);
        Task EliminarAsync(Cuenta cuenta);
    }
}
