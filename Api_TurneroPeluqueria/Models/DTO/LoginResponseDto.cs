﻿namespace Api_TurneroPeluqueria.Models.DTO
{
    public class LoginResponseDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int IdRol { get; set; }
    }
}
