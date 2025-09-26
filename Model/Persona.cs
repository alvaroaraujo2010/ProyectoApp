
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoApp.Model
{
    [Table("persona")]
    public class Persona
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("genero")]
        public string? Genero { get; set; }

        [Column("edad")]
        public int? Edad { get; set; }

        [Column("direccion")]
        public string? Direccion { get; set; }

        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("identificacion")]
        public string? Identificacion { get; set; }
    }
}
