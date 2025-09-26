using ProyectoApp.Model;

namespace ProyectoApp.Service
{
    public interface IMovimientoService
    {
        Task<Movimiento> CreateAsync(string numeroCuenta, string tipo, decimal valor, string referencia, string tipoMovimiento);
        Task<List<Movimiento>> ListByCuentaAsync(string numeroCuenta, DateTime from, DateTime to);
        Task<decimal> SaldoActualAsync(string numeroCuenta);
        Task<List<Movimiento>> ListWithSaldoAsync(string numeroCuenta, DateTime from, DateTime to);
        Task<List<Movimiento>> ListAllWithSaldoAsync();
        Task<EstadoCuentaReporte> GenerarReporteAsync(string numeroCuenta, DateTime from, DateTime to);
        Task<string> ExportarReportePDFBase64Async(EstadoCuentaReporte reporte);
        Task<List<Movimiento>> ListarMovimientosConSaldoAsync(string numeroCuenta, DateTime desde, DateTime hasta);
    }
}
