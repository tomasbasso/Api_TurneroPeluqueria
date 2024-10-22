using Api_TurneroPeluqueria.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_TurneroPeluqueria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Api_TurneroPeluqueria.Models.DTO;

namespace Api_TurneroPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase 
    {
        private readonly TurneroDbContext _context;

        public EmpleadoController(TurneroDbContext context)
        {
            _context = context;
        }
        ////////BUSCAR TODOS//////////////////////////AUTOMAPER
        [HttpGet("ObtenerEmpleados")]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var lista = await _context.Empleados.Select(u => new VerEmpleadosDTO
                {
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Telefono = u.Telefono,
                    Email = u.Email,
                    Especialidad = u.Especialidad
                })
                    .ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CrearEmpleado")]
        public async Task<IActionResult> Crear(CrearEmpleadoDTO empleadoDTO)
        {
            try
            {
               
                var empleado = new Empleado
                {

                    Nombre = empleadoDTO.Nombre,
                    Apellido = empleadoDTO.Apellido,
                    Telefono = empleadoDTO.Telefono,
                    Email = empleadoDTO.Email,
                    Contraseña = empleadoDTO.Contraseña,
                    Especialidad = empleadoDTO.Especialidad
                 
                   
                };

                await _context.Empleados.AddAsync(empleado);
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
                var item = await _context.Empleados.FindAsync(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //////////BORRAR//////////
        [HttpDelete("Borrar por {id:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int id)
        {
            try
            {
                var EmpleadoExistente = await _context.Empleados.FindAsync(id);

                if (EmpleadoExistente != null)
                {
                    _context.Empleados.Remove(EmpleadoExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Modificar por {id:int}")]
        public async Task<IActionResult> Modificar([FromBody] ModificarEmpleadoDTO empleado, [FromRoute] int id)
        {
            try
            {
                var EmpleadoExistente = await _context.Empleados.FindAsync(id);

                if (EmpleadoExistente != null)
                {
                    if (!empleado.Nombre.IsNullOrEmpty()) EmpleadoExistente.Nombre = empleado.Nombre;
                    if (!empleado.Apellido.IsNullOrEmpty()) EmpleadoExistente.Apellido = empleado.Apellido;
                    if (!empleado.Email.IsNullOrEmpty()) EmpleadoExistente.Email = empleado.Email;
                    if (!empleado.Contraseña.IsNullOrEmpty()) EmpleadoExistente.Contraseña = empleado.Contraseña;
                    if (!empleado.Telefono.IsNullOrEmpty()) EmpleadoExistente.Telefono = empleado.Telefono;
                    if (!empleado.Especialidad.IsNullOrEmpty()) EmpleadoExistente.Especialidad = empleado.Especialidad;


                    _context.Empleados.Update(EmpleadoExistente);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("ValidarCredencial")]
        public async Task<IActionResult> ValidarCredencial([FromBody] EmpleadoLoginDTO empleado)
        {
            var existeLogin = await _context.Empleados
            .AnyAsync(x => x.Email.Equals(empleado.Email) && x.Contraseña.Equals(empleado.Contraseña));

            Empleado empleadoLogin = await _context.Empleados.FirstOrDefaultAsync(x => x.Email.Equals(empleado.Email) && x.Contraseña.Equals(empleado.Contraseña));

            if (existeLogin == null)
            {
                // Si no se encuentra el usuario, retornar un mensaje de error
                return NotFound("Usuario o contraseña incorrectos");
            }

            // Crear una respuesta con los datos del usuario autenticado
            EmpleadoLoginDTO EmpleadoResponse = new EmpleadoLoginDTO()
            {
                
                Email = existeLogin ? empleadoLogin.Email : "",
                Contraseña = existeLogin ? empleadoLogin.Contraseña : ""

            };

            // Retornar la respuesta con los datos del usuario
            return Ok(EmpleadoResponse);
        }
    }
}

