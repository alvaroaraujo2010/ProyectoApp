
using ProyectoApp.Model;

namespace ProyectoApp.Service
{
    public interface ICuentaService
    {
        Task<Cuenta> CrearAsync(Cuenta cuenta);
        Task<List<Cuenta>> ListarAsync();
        Task<Cuenta?> ObtenerPorNumeroAsync(string numero);
        Task<Cuenta?> ActualizarAsync(Cuenta cuenta);
        Task<bool> EliminarAsync(long id);
    }
}
