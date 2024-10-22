using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models.DTO
{
    public class CrearTurnoDTO
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int ServicioId { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "La fecha y hora del turno es obligatoria")]
        public DateTime FechaHora { get; set; }

        public string Observaciones { get; set; }
    }
}
