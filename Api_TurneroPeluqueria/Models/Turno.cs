using Api_TurneroPeluqueria.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Api_TurneroPeluqueria.Models
{
    public class Turno
    {
        public int IdTurno { get; set; }

        // Relación con Usuario (Cliente)
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        // Relación con Usuario (Peluquero)
        public int IdPeluquero { get; set; }
        public Usuario Peluquero { get; set; }

        // Relación con Servicio
        public int IdServicio { get; set; }
        public Servicio Servicio { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Estado { get; set; }

        // Relación con Pagos
        public ICollection<Pago> Pagos { get; set; }
    }
}