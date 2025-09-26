using Microsoft.EntityFrameworkCore;
using ProyectoApp.Data;
using ProyectoApp.Model;

namespace ProyectoApp.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _ctx;
        public ClienteRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Cliente cliente)
        {
            _ctx.Cliente.Add(cliente);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Cliente?> FindByIdAsync(long id)
        {
            return await _ctx.Cliente.FindAsync(id);
        }

        public async Task<Cliente?> FindByIdentificacionAsync(string identificacion)
        {
            return await _ctx.Cliente.FirstOrDefaultAsync(c => c.Identificacion == identificacion);
        }

        public async Task<List<Cliente>> FindAllAsync()
        {
            return await _ctx.Cliente.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task ActualizarAsync(Cliente cliente)
        {
            _ctx.Cliente.Update(cliente);
            await _ctx.SaveChangesAsync();
        }

        public async Task EliminarAsync(Cliente cliente)
        {
            _ctx.Cliente.Remove(cliente);
            await _ctx.SaveChangesAsync();
        }
    }
}
