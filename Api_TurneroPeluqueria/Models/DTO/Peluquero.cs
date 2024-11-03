using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models.DTO
{
    public class Peluquero
    {
        [Key]
        public int IdPeluquero { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "La especialización es obligatoria.")]
        [StringLength(100, ErrorMessage = "La especialización no puede superar los 100 caracteres.")]
        public string Especializacion { get; set; }

        [Range(0, 50, ErrorMessage = "La experiencia debe ser un número entre 0 y 50 años.")]
        public int Experiencia { get; set; }

        // Navegación a la tabla Usuarios
        public virtual Usuario Usuario { get; set; }
    }
}
