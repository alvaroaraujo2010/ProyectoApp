using ProyectoApp.Model;

public interface IClienteRepository
{
    Task AddAsync(Cliente cliente);
    Task<Cliente?> FindByIdAsync(long id);
    Task<Cliente?> FindByIdentificacionAsync(string identificacion);
    Task<List<Cliente>> FindAllAsync();
    Task ActualizarAsync(Cliente cliente);
    Task EliminarAsync(Cliente cliente);
}
