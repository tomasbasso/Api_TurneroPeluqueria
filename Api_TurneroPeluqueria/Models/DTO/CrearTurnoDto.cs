using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models.DTO
{
    public class CrearTurnoDto
    {

        public int IdUsuario { get; set; }

        public int IdPeluquero { get; set; }

        public int IdServicio { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Estado { get; set; }


    }
}
