
using ProyectoApp.Model;
using ProyectoApp.Repository;

namespace ProyectoApp.Service
{
    public class ClienteServiceImpl : IClienteService
    {
        private readonly IClienteRepository _repo;
        public ClienteServiceImpl(IClienteRepository repo) { _repo = repo; }

        public async Task<Cliente> CrearAsync(Cliente cliente)
        {
            await _repo.AddAsync(cliente);
            return cliente;
        }

        public async Task<List<Cliente>> ListarAsync() => await _repo.FindAllAsync();
    }
}
