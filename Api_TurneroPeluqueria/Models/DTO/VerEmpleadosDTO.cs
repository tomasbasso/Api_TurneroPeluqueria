using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models.DTO
{
    public class VerEmpleadosDTO
    {
        

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public string Especialidad { get; set; }

        public DateTime FechaContratacion { get; set; } = DateTime.Now;

        // Relación: un empleado puede tener varios turnos
        public List<Turno> Turnos { get; set; }
    }
}

