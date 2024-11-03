namespace Api_TurneroPeluqueria.Models.DTO
{
    public class VerUsuariosDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        // Relación con Rol
        public int IdRol { get; set; }


    }
}
