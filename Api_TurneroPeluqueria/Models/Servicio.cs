using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        // Relación con Turnos
        public ICollection<Turno> Turnos { get; set; }
    }

}
