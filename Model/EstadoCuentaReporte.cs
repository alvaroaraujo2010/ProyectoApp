using ProyectoApp.Model;
using System;
using System.Collections.Generic;

namespace ProyectoApp.Model
{
    public class EstadoCuentaReporte
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TipoCuenta { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public List<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
        public decimal TotalCreditos { get; set; }
        public decimal TotalDebitos { get; set; }
        public decimal SaldoFinal { get; set; }
    }
}
