
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
    }
}
