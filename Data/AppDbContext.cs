
using Microsoft.EntityFrameworkCore;
using ProyectoApp.Model;

namespace ProyectoApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<Movimiento> Movimiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Persona>().ToTable("persona");
            modelBuilder.Entity<Cliente>().ToTable("cliente");
            modelBuilder.Entity<Cuenta>().ToTable("cuenta");
            modelBuilder.Entity<Movimiento>().ToTable("movimiento");
        }
    }
}
