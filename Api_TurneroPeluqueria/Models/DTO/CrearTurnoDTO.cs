using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models.DTO
{
  
        public class CrearTurnoDTO
        {
            public DateTime Fecha { get; set; }
            public string ClienteNombre { get; set; } 
            public string ServicioNombre { get; set; } 
            public string EmpleadoNombre { get; set; } 
            public string Observaciones { get; set; }
        }
    
}
