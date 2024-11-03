namespace Api_TurneroPeluqueria.Models
{
    public class Pago
    {
        public int IdPago { get; set; }

        // Relación con Turno
        public int IdTurno { get; set; }
        public Turno Turno { get; set; }

        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
    }
}
