using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }

        // Relación con Usuario
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        // Relación con Turno
        public int IdTurno { get; set; }
        public Turno Turno { get; set; }
    }

}
