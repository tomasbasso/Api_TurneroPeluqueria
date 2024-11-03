using Api_TurneroPeluqueria.Data;
using Api_TurneroPeluqueria.Models;
using Api_TurneroPeluqueria.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api_TurneroPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly TurneroPeluqueriaContext _context;

        public ServicioController(TurneroPeluqueriaContext context)
        {
            _context = context;
        }
        ////////BUSCAR TODOS//////////////////////////AUTOMAPER
        [HttpGet("ObtenerServicios")]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var lista = await _context.Servicios.Select(u => new Servicio
                {
                    IdServicio=u.IdServicio,
                    Nombre = u.Nombre,
                    Descripcion = u.Descripcion,
                    Precio = u.Precio,


                })
                    .ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CrearServicio")]
        public async Task<IActionResult> Crear(CrearServicioDTO servicioDTO)
        {
            try
            {

                var servicio = new Servicio
                {

                    Nombre = servicioDTO.Nombre,
                    Descripcion = servicioDTO.Descripcion,
                    Precio = servicioDTO.Precio,

                };

                await _context.Servicios.AddAsync(servicio);
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
                var item = await _context.Servicios.FindAsync(id);
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
                var ServicioExistente = await _context.Servicios.FindAsync(id);

                if (ServicioExistente != null)
                {
                    _context.Servicios.Remove(ServicioExistente);
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
        public async Task<IActionResult> Modificar([FromBody] CrearServicioDTO servicio, [FromRoute] int id)
        {
            try
            {
                var servicioExistente = await _context.Servicios.FindAsync(id);

                if (servicioExistente != null)
                {
                    if (!string.IsNullOrEmpty(servicio.Nombre)) servicioExistente.Nombre = servicio.Nombre;
                    if (!string.IsNullOrEmpty(servicio.Nombre)) servicioExistente.Descripcion = servicio.Descripcion;
                    if (!string.IsNullOrEmpty(servicio.Nombre)) servicioExistente.Precio = servicio.Precio;


                    _context.Servicios.Update(servicioExistente);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
