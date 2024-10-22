using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models.DTO
{
    public class EmpleadoLoginDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Contraseña { get; set; }
    }
}
