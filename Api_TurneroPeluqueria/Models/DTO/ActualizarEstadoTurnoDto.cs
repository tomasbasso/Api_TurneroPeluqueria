using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models.DTO
{
    public class ActualizarEstadoTurnoDto
    {
        [Required]
        [StringLength(50)]
        public string Estado { get; set; } // Pendiente, Confirmado, Cancelado
    }
}
