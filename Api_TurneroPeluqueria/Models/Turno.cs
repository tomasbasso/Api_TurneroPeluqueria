using Api_TurneroPeluqueria.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Api_TurneroPeluqueria.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }

        // Relación con Usuario (Cliente)
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("TurnosCliente")]
        public Usuario Usuario { get; set; }

        // Relación con Usuario (Peluquero)
        public int IdPeluquero { get; set; }

        [ForeignKey("IdPeluquero")]
        [InverseProperty("TurnosPeluquero")]
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