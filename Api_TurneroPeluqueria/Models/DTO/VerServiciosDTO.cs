using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models.DTO
{
    public class VerServiciosDTO
    {
        [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La duración del servicio es obligatoria")]
        public int Duracion { get; set; } // Duración en minutos

        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Precio { get; set; }

    }
}
