using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoApp.Model
{
    [Table("movimiento")]
    public class Movimiento
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Column("tipoMovimiento")]
        public string TipoMovimiento { get; set; } = string.Empty;

        [Column("valor")]
        public decimal Valor { get; set; }

        [Column("saldo")]
        public decimal Saldo { get; set; }

        [Column("referencia")]
        public string? Referencia { get; set; }

        [Column("cuenta_id")]
        public long CuentaId { get; set; }

        public Cuenta? Cuenta { get; set; }
    }
}
