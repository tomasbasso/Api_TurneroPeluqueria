using Api_TurneroPeluqueria.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_TurneroPeluqueria.Models;
using Microsoft.EntityFrameworkCore;
using Api_TurneroPeluqueria.Models.DTO;

namespace Api_TurneroPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly TurneroPeluqueriaContext _context;

        public UsuarioController(TurneroPeluqueriaContext context)
        {
            _context = context;
        }

        ////////BUSCAR TODOS//////////////////////////AUTOMAPER
        [HttpGet("ObtenerUsuarios")]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var lista = await _context.Usuarios.Select(u => new VerUsuariosDTO
                {
                    IdUsuario=u.IdUsuario,
                    Nombre = u.Nombre,
                    Email= u.Email,
                    Telefono= u.Telefono,
                    IdRol=u.IdRol,

                })
                .ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> Crear(CrearUsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nombre = usuarioDTO.Nombre,
                    Email = usuarioDTO.Email,
                    Contraseña=usuarioDTO.Contraseña,
                    Telefono= usuarioDTO.Telefono,
                    IdRol = usuarioDTO.IdRol,
                };

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }

        ////////BUSCAR POR ID//////////////////////////
        [HttpGet("ObtenerPorId/{id:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "id")] int id)
        {
            try
            {
                var item = await _context.Usuarios.FindAsync(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //////////BORRAR//////////
        [HttpDelete("Borrar/{id:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int id)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(id);

                if (usuarioExistente != null)
                {
                    _context.Usuarios.Remove(usuarioExistente);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }

                return NotFound("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Modificar/{id:int}")]
        public async Task<IActionResult> Modificar([FromBody] CrearUsuarioDTO usuario, [FromRoute] int id)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(id);

                if (usuarioExistente != null)
                {
                    if (!string.IsNullOrEmpty(usuario.Nombre)) usuarioExistente.Nombre = usuario.Nombre;
                    if (!string.IsNullOrEmpty(usuario.Email)) usuarioExistente.Email = usuario.Email;
                    if (!string.IsNullOrEmpty(usuario.Contraseña)) usuarioExistente.Contraseña = usuario.Contraseña;
                    if (!string.IsNullOrEmpty(usuario.Telefono)) usuarioExistente.Telefono = usuario.Telefono;
                    usuarioExistente.IdRol = usuario.IdRol;

                    _context.Usuarios.Update(usuarioExistente);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }

                return NotFound("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ValidarCredencial")]
        public async Task<IActionResult> ValidarCredencial([FromBody] UsuarioLoginDTO usuario)
        {
            var existeLogin = await _context.Usuarios
                .AnyAsync(x => x.Email.Equals(usuario.Email) && x.Contraseña.Equals(usuario.Contraseña));

            if (!existeLogin)
            {
                return NotFound("Usuario o contraseña incorrectos");
            }

            var usuarioLogin = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.Equals(usuario.Email));

            // Crear una respuesta con los datos del usuario autenticado
            UsuarioLoginDTO usuarioResponse = new UsuarioLoginDTO()
            {
                Email = usuarioLogin.Email,
                IdRol = usuarioLogin.IdRol
            };

            // Retornar la respuesta con los datos del usuario
            return Ok(usuarioResponse);
        }
    }
}
