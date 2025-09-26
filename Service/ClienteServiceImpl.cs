using ProyectoApp.Model;
using ProyectoApp.Repository;

namespace ProyectoApp.Service
{
    public class ClienteServiceImpl : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteServiceImpl(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<Cliente> CrearAsync(Cliente cliente)
        {
            await _repo.AddAsync(cliente);
            return cliente;
        }

        public async Task<List<Cliente>> ListarAsync()
        {
            return await _repo.FindAllAsync();
        }

        public async Task<Cliente?> ObtenerPorIdAsync(long id)
        {
            return await _repo.FindByIdAsync(id);
        }

        public async Task<Cliente> ActualizarAsync(Cliente cliente)
        {
            await _repo.ActualizarAsync(cliente);
            return cliente;
        }

        public async Task<bool> EliminarAsync(long id)
        {
            var cliente = await _repo.FindByIdAsync(id);
            if (cliente == null)
                return false;

            await _repo.EliminarAsync(cliente);
            return true;
        }
    }
}
