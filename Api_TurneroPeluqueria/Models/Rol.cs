using System.ComponentModel.DataAnnotations;

namespace Api_TurneroPeluqueria.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string Nombre { get; set; }

        // Relación con Usuarios
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
