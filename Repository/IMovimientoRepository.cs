
using ProyectoApp.Model;

namespace ProyectoApp.Repository
{
    public interface IMovimientoRepository
    {
        Task SaveAsync(Movimiento movimiento);
        Task<List<Movimiento>> FindByCuentaAndFechaBetweenAsync(Cuenta cuenta, DateTime from, DateTime to);
        Task<List<Movimiento>> FindByCuentaAsync(Cuenta cuenta);
        Task<List<Movimiento>> FindAllAsync();
    }
}
