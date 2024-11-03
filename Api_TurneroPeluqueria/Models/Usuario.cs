using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_TurneroPeluqueria.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }

        // Relación con Rol
        public int IdRol { get; set; }
        public Rol Rol { get; set; }

        // Relaciones con Turnos
        public ICollection<Turno> TurnosCliente { get; set; }  // Turnos como cliente
        public ICollection<Turno> TurnosPeluquero { get; set; } // Turnos como peluquero

        // Relaciones con Horarios Disponibles
        public ICollection<HorarioDisponible> HorariosDisponibles { get; set; }
    }
}
