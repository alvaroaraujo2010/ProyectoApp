
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoApp.Model
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("email")]
        public string? Email { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("identificacion")]
        public string? Identificacion { get; set; }

        public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
    }
}
