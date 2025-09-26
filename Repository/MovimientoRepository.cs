
using Microsoft.EntityFrameworkCore;
using ProyectoApp.Data;
using ProyectoApp.Model;

namespace ProyectoApp.Repository
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly AppDbContext _ctx;
        public MovimientoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task SaveAsync(Movimiento movimiento) { _ctx.Movimiento.Add(movimiento); await _ctx.SaveChangesAsync(); }

        public async Task<List<Movimiento>> FindByCuentaAndFechaBetweenAsync(Cuenta cuenta, DateTime from, DateTime to)
            => await _ctx.Movimiento.Where(m=>m.CuentaId==cuenta.Id && m.Fecha>=from && m.Fecha<=to).OrderBy(m=>m.Fecha).ToListAsync();

        public async Task<List<Movimiento>> FindByCuentaAsync(Cuenta cuenta)
            => await _ctx.Movimiento.Where(m=>m.CuentaId==cuenta.Id).OrderBy(m=>m.Fecha).ToListAsync();

        public async Task<List<Movimiento>> FindAllAsync()
            => await _ctx.Movimiento.Include(m=>m.Cuenta).ThenInclude(c=>c.Cliente).OrderBy(m=>m.Fecha).ToListAsync();
    }
}
