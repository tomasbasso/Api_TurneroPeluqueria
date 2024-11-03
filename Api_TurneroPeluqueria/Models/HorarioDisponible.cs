using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models
{
    public class HorarioDisponible
    {
        [Key]
        public int IdHorario { get; set; }

        // Relación con Usuario (Peluquero)
        public int IdPeluquero { get; set; }
        public Usuario Peluquero { get; set; }

        public int DiaSemana { get; set; }  // 0 = Domingo, 1 = Lunes, etc.
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
