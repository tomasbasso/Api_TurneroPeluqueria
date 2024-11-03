using Api_TurneroPeluqueria.Data;
using Api_TurneroPeluqueria.Models;
using Api_TurneroPeluqueria.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TurnoController : ControllerBase
{
    private readonly TurneroDbContext _context;

    public TurnoController(TurneroDbContext context)
    {
        _context = context;
    }

    // GET: api/Turnos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Turno>>> GetTurnos()
    {
        return await _context.Turnos
            .Include(t => t.Usuario)
            .Include(t => t.Peluquero)
            .Include(t => t.Servicio)
            .ToListAsync();
    }

    // GET: api/Turnos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Turno>> GetTurno(int id)
    {
        var turno = await _context.Turnos
            .Include(t => t.Usuario)
            .Include(t => t.Peluquero)
            .Include(t => t.Servicio)
            .FirstOrDefaultAsync(t => t.IdTurno == id);

        if (turno == null)
        {
            return NotFound();
        }

        return turno;
    }

    // GET: api/Turnos/Cliente/5
    [HttpGet("Cliente/{idCliente}")]
    public async Task<ActionResult<IEnumerable<Turno>>> GetTurnosPorCliente(int idCliente)
    {
        var turnos = await _context.Turnos
            .Where(t => t.IdUsuario == idCliente)
            .Include(t => t.Servicio)
            .Include(t => t.Peluquero)
            .ToListAsync();

        return Ok(turnos);
    }

    // GET: api/Turnos/Peluquero/5
    [HttpGet("Peluquero/{idPeluquero}")]
    public async Task<ActionResult<IEnumerable<Turno>>> GetTurnosPorPeluquero(int idPeluquero)
    {
        var turnos = await _context.Turnos
            .Where(t => t.IdPeluquero == idPeluquero)
            .Include(t => t.Servicio)
            .Include(t => t.Usuario)
            .ToListAsync();

        return Ok(turnos);
    }

    [HttpPost("CrearServicio")]
    public async Task<IActionResult> Crear(CrearTurnoDto turnodto)
    {
        try
        {

            var turno = new Turno
            {

                IdUsuario = turnodto.IdUsuario,
                IdPeluquero = turnodto.IdPeluquero,
                IdServicio = turnodto.IdServicio,
                Fecha = turnodto.Fecha,
                Hora = turnodto.Hora,
                Estado = turnodto.Estado

            };

            await _context.Turnos.AddAsync(turno);
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

    // PUT: api/Turnos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTurno(int id, Turno turno)
    {
        if (id != turno.IdTurno)
        {
            return BadRequest();
        }

        _context.Entry(turno).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TurnoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Turnos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTurno(int id)
    {
        var turno = await _context.Turnos.FindAsync(id);
        if (turno == null)
        {
            return NotFound();
        }

        _context.Turnos.Remove(turno);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TurnoExists(int id)
    {
        return _context.Turnos.Any(e => e.IdTurno == id);
    }
}
