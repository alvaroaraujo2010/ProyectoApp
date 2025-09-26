
using Microsoft.EntityFrameworkCore;
using ProyectoApp.Data;
using ProyectoApp.Model;

namespace ProyectoApp.Repository
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly AppDbContext _ctx;
        public CuentaRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<Cuenta?> FindByIdAsync(long id)
        {
            return await _ctx.Cuenta.FindAsync(id);
        }

        public async Task AddAsync(Cuenta cuenta) { 
            _ctx.Cuenta.Add(cuenta); await _ctx.SaveChangesAsync(); 
        }
        public async Task<Cuenta?> FindByNumeroAsync(string numero) => await _ctx.Cuenta.Include(c=>c.Cliente).FirstOrDefaultAsync(c=>c.Numero==numero);
        public async Task<List<Cuenta>> FindAllAsync() => await _ctx.Cuenta.Include(c=>c.Cliente).ToListAsync();

        public async Task ActualizarAsync(Cuenta cuenta)
        {
            _ctx.Cuenta.Update(cuenta);
            await _ctx.SaveChangesAsync();
        }

        public async Task EliminarAsync(Cuenta cuenta)
        {
            _ctx.Cuenta.Remove(cuenta);
            await _ctx.SaveChangesAsync();
        }
    }
}
