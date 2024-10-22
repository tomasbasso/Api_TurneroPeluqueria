using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La duración del servicio es obligatoria")]
        public int Duracion { get; set; } // Duración en minutos

        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Precio { get; set; }

        // Relación: un servicio puede estar en varios turnos
        public List<Turno> Turnos { get; set; }
    }

}
