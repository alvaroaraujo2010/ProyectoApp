using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoApp.Model
{
    [Table("cuenta")]
    public class Cuenta
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("numero")]
        public string Numero { get; set; } = string.Empty;

        [Column("tipo")]
        public string? Tipo { get; set; }

        [Column("saldo_inicial")]
        public decimal SaldoInicial { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        [Column("cliente_id")]
        public long? ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}
