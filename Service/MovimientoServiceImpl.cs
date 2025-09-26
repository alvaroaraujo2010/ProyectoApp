using ProyectoApp.Model;
using ProyectoApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoApp.Service
{
    public class MovimientoServiceImpl : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly decimal LIMITE_DIARIO = 1000m;

        public MovimientoServiceImpl(IMovimientoRepository movimientoRepository, ICuentaRepository cuentaRepository)
        {
            _movimientoRepository = movimientoRepository;
            _cuentaRepository = cuentaRepository;
        }

        public async Task<Movimiento> CreateAsync(string numeroCuenta, string tipo, decimal valor, string referencia, string tipoMovimiento)
        {
            var cuenta = await _cuentaRepository.FindByNumeroAsync(numeroCuenta)
                ?? throw new Exception("Cuenta no encontrada");

            var current = await SaldoActualAsync(numeroCuenta);

            if (tipo.Equals("DEBITO", StringComparison.OrdinalIgnoreCase) && current < valor)
            {
                throw new Exception("Saldo insuficiente");
            }

            var movimiento = new Movimiento
            {
                CuentaId = cuenta.Id,
                Tipo = tipo.ToUpper(),
                TipoMovimiento = tipoMovimiento, // ?? Nuevo campo
                Valor = valor,
                Referencia = string.IsNullOrEmpty(referencia) ? Guid.NewGuid().ToString() : referencia,
                Saldo = tipo.Equals("DEBITO", StringComparison.OrdinalIgnoreCase) ? current - valor : current + valor,
                Fecha = DateTime.UtcNow
            };

            await _movimientoRepository.SaveAsync(movimiento);
            return movimiento;
        }

        public async Task<List<Movimiento>> ListByCuentaAsync(string numeroCuenta, DateTime from, DateTime to)
        {
            var cuenta = await _cuentaRepository.FindByNumeroAsync(numeroCuenta)
                ?? throw new Exception("Cuenta no encontrada");

            return await _movimientoRepository.FindByCuentaAndFechaBetweenAsync(cuenta, from, to);
        }

        public async Task<decimal> SaldoActualAsync(string numeroCuenta)
        {
            var cuenta = await _cuentaRepository.FindByNumeroAsync(numeroCuenta)
                ?? throw new Exception("Cuenta no encontrada");

            var inicio = cuenta.SaldoInicial;

            var movimientos = await _movimientoRepository.FindByCuentaAndFechaBetweenAsync(
                cuenta,
                new DateTime(1970, 1, 1),
                DateTime.UtcNow
            );

            var movimientosSum = movimientos.Sum(m =>
                m.Tipo.Equals("DEBITO", StringComparison.OrdinalIgnoreCase) ? -m.Valor : m.Valor
            );

            return inicio + movimientosSum;
        }

        public async Task<List<Movimiento>> ListWithSaldoAsync(string numeroCuenta, DateTime from, DateTime to)
        {
            var cuenta = await _cuentaRepository.FindByNumeroAsync(numeroCuenta)
                ?? throw new Exception("Cuenta no encontrada");

            decimal saldo = cuenta.SaldoInicial;
            var movimientos = await _movimientoRepository.FindByCuentaAndFechaBetweenAsync(cuenta, from, to);

            foreach (var m in movimientos.OrderBy(m => m.Fecha))
            {
                if (m.Tipo.Equals("DEBITO", StringComparison.OrdinalIgnoreCase))
                    saldo -= m.Valor;
                else
                    saldo += m.Valor;

                m.Saldo = saldo;
            }

            return movimientos;
        }

        public async Task<List<Movimiento>> ListAllWithSaldoAsync()
        {
            var cuentas = await _cuentaRepository.FindAllAsync();
            var result = new List<Movimiento>();

            foreach (var cuenta in cuentas)
            {
                decimal saldo = cuenta.SaldoInicial;
                var movimientos = await _movimientoRepository.FindByCuentaAndFechaBetweenAsync(
                    cuenta,
                    new DateTime(1970, 1, 1),
                    DateTime.UtcNow
                );

                foreach (var m in movimientos.OrderBy(m => m.Fecha))
                {
                    if (m.Tipo.Equals("DEBITO", StringComparison.OrdinalIgnoreCase))
                        saldo -= m.Valor;
                    else
                        saldo += m.Valor;

                    m.Saldo = saldo;
                    result.Add(m);
                }
            }

            return result.OrderBy(m => m.Fecha).ToList();
        }

        public async Task<EstadoCuentaReporte> GenerarReporteAsync(string numeroCuenta, DateTime from, DateTime to)
        {
            var cuenta = await _cuentaRepository.FindByNumeroAsync(numeroCuenta)
                ?? throw new Exception("Cuenta no encontrada");

            var movimientos = await ListWithSaldoAsync(numeroCuenta, from, to);

            var totalCreditos = movimientos
                .Where(m => m.Tipo.Equals("CREDITO", StringComparison.OrdinalIgnoreCase))
                .Sum(m => m.Valor);

            var totalDebitos = movimientos
                .Where(m => m.Tipo.Equals("DEBITO", StringComparison.OrdinalIgnoreCase))
                .Sum(m => m.Valor);

            return new EstadoCuentaReporte
            {
                NumeroCuenta = cuenta.Numero,
                TipoCuenta = cuenta.Tipo,
                Cliente = cuenta.Cliente?.Nombre ?? string.Empty,
                FechaDesde = from,
                FechaHasta = to,
                Movimientos = movimientos,
                TotalCreditos = totalCreditos,
                TotalDebitos = totalDebitos,
                SaldoFinal = await SaldoActualAsync(numeroCuenta)
            };
        }

        public Task<string> ExportarReportePDFBase64Async(EstadoCuentaReporte reporte)
        {
            // Placeholder PDF vacío
            using var ms = new MemoryStream();
            byte[] pdfBytes = ms.ToArray();
            return Task.FromResult(Convert.ToBase64String(pdfBytes));
        }

        public async Task<List<Movimiento>> ListarMovimientosConSaldoAsync(string numeroCuenta, DateTime desde, DateTime hasta)
        {
            var cuenta = await _cuentaRepository.FindByNumeroAsync(numeroCuenta)
                ?? throw new Exception("Cuenta no encontrada");

            var movimientos = await _movimientoRepository.FindByCuentaAndFechaBetweenAsync(cuenta, desde, hasta);

            decimal saldoActual = cuenta.SaldoInicial;
            decimal totalDebitosHoy = 0m;
            DateTime? fechaActual = null;

            foreach (var m in movimientos.OrderBy(m => m.Fecha))
            {
                if (fechaActual == null || m.Fecha.Date != fechaActual.Value.Date)
                {
                    totalDebitosHoy = 0m;
                    fechaActual = m.Fecha;
                }

                if (m.Tipo.Equals("DEBITO", StringComparison.OrdinalIgnoreCase))
                {
                    if (saldoActual <= 0)
                    {
                        m.Referencia = "Saldo no disponible";
                        m.Saldo = saldoActual;
                        continue;
                    }

                    if (totalDebitosHoy + m.Valor > LIMITE_DIARIO)
                    {
                        m.Referencia = "Cupo diario excedido";
                        m.Saldo = saldoActual;
                        continue;
                    }

                    saldoActual -= m.Valor;
                    totalDebitosHoy += m.Valor;
                }
                else
                {
                    saldoActual += m.Valor;
                }

                m.Saldo = saldoActual;
            }

            return movimientos;
        }
    }
}
