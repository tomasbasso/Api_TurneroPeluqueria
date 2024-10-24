using Api_TurneroPeluqueria.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_TurneroPeluqueria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Api_TurneroPeluqueria.Models.DTO;

namespace Api_TurneroPeluqueria.Controllers
{
    public class TurnoController : ControllerBase
    {
        private readonly TurneroDbContext _context;

    public TurnoController(TurneroDbContext context)
    {
        _context = context;
    }
    
        [HttpGet("ObtenerClientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            try
            {
                var clientes = await _context.Clientes
                    .Select(c => new { c.Id, c.Nombre })
                    .ToListAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerServicios")]
        public async Task<IActionResult> ObtenerServicios()
        {
            try
            {
                var servicios = await _context.Servicios
                    .Select(s => new { s.Id, s.Nombre })
                    .ToListAsync();
                return Ok(servicios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerEmpleados")]
        public async Task<IActionResult> ObtenerEmpleados()
        {
            try
            {
                var empleados = await _context.Empleados
                    .Select(e => new { e.Id, e.Nombre })
                    .ToListAsync();
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CrearTurno")]
        public async Task<IActionResult> CrearTurno(CrearTurnoDTO turnoDTO)
        {
            try
            {
                
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Nombre == turnoDTO.ClienteNombre);
                if (cliente == null) return BadRequest("Cliente no encontrado");

                
                var servicio = await _context.Servicios.FirstOrDefaultAsync(s => s.Nombre == turnoDTO.ServicioNombre);
                if (servicio == null) return BadRequest("Servicio no encontrado");

                
                var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Nombre == turnoDTO.EmpleadoNombre);
                if (empleado == null) return BadRequest("Empleado no encontrado");

              
                var nuevoTurno = new Turno
                {
                    FechaHora = turnoDTO.Fecha,
                    ClienteId = cliente.Id,
                    ServicioId = servicio.Id,
                    EmpleadoId = empleado.Id,
                    Observaciones = turnoDTO.Observaciones 
                };

                await _context.Turnos.AddAsync(nuevoTurno);
                await _context.SaveChangesAsync();

                return Ok(nuevoTurno);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int id)
        {
            try
            {
                var turnoExistente = await _context.Turnos.FindAsync(id);

                if (turnoExistente == null)
                {
                    return NotFound(); 
                }

                // Elimina el turno
                _context.Turnos.Remove(turnoExistente);
                await _context.SaveChangesAsync();

                return NoContent(); 
            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException?.Message;
                return BadRequest($"Error al eliminar el turno: {innerException}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}"); // Retorna 400 Bad Request en caso de error
            }
        }


    }
}
