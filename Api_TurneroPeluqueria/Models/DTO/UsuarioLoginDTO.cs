namespace Api_TurneroPeluqueria.Models.DTO
{
    public class UsuarioLoginDTO
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int IdRol { get; set; }

    }
}
