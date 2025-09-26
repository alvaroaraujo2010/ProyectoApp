
using ProyectoApp.Model;
using ProyectoApp.Repository;

namespace ProyectoApp.Service
{
    public class CuentaServiceImpl : ICuentaService
    {
        private readonly ICuentaRepository _repo;
        public CuentaServiceImpl(ICuentaRepository repo) { _repo = repo; }

        public async Task<Cuenta> CrearAsync(Cuenta cuenta)
        {
            await _repo.AddAsync(cuenta);
            return cuenta;
        }

        public async Task<List<Cuenta>> ListarAsync() => await _repo.FindAllAsync();

        public async Task<Cuenta?> ObtenerPorNumeroAsync(string numero) => await _repo.FindByNumeroAsync(numero);

        public async Task<Cuenta> ActualizarAsync(Cuenta cuenta)
        {
            await _repo.ActualizarAsync(cuenta);
            return cuenta;
        }

        public async Task<bool> EliminarAsync(long id)
        {
            var cuenta = await _repo.FindByIdAsync(id);
            if (cuenta == null)
                return false;

            await _repo.EliminarAsync(cuenta);
            return true;
        }
    }
}
