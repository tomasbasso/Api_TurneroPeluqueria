using Api_TurneroPeluqueria.Data;
using Api_TurneroPeluqueria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class HorarioDisponibleController : ControllerBase
{
    private readonly TurneroDbContext _context;

    public HorarioDisponibleController(TurneroDbContext context)
    {
        _context = context;
    }

    // GET: api/HorariosDisponibles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HorarioDisponible>>> GetHorariosDisponibles()
    {
        return await _context.HorariosDisponibles.Include(h => h.Peluquero).ToListAsync();
    }

    // GET: api/HorariosDisponibles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HorarioDisponible>> GetHorarioDisponible(int id)
    {
        var horarioDisponible = await _context.HorariosDisponibles
            .Include(h => h.Peluquero)
            .FirstOrDefaultAsync(h => h.IdHorario == id);

        if (horarioDisponible == null)
        {
            return NotFound();
        }

        return horarioDisponible;
    }

    // GET: api/HorariosDisponibles/Peluquero/5
    [HttpGet("Peluquero/{idPeluquero}")]
    public async Task<ActionResult<IEnumerable<HorarioDisponible>>> GetHorariosPorPeluquero(int idPeluquero)
    {
        var horarios = await _context.HorariosDisponibles
            .Where(h => h.IdPeluquero == idPeluquero)
            .ToListAsync();

        return Ok(horarios);
    }

    // POST: api/HorariosDisponibles
    [HttpPost]
    public async Task<ActionResult<HorarioDisponible>> PostHorarioDisponible(HorarioDisponible horarioDisponible)
    {
        _context.HorariosDisponibles.Add(horarioDisponible);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetHorarioDisponible", new { id = horarioDisponible.IdHorario }, horarioDisponible);
    }

    // PUT: api/HorariosDisponibles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHorarioDisponible(int id, HorarioDisponible horarioDisponible)
    {
        if (id != horarioDisponible.IdHorario)
        {
            return BadRequest();
        }

        _context.Entry(horarioDisponible).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HorarioDisponibleExists(id))
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

    // DELETE: api/HorariosDisponibles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHorarioDisponible(int id)
    {
        var horarioDisponible = await _context.HorariosDisponibles.FindAsync(id);
        if (horarioDisponible == null)
        {
            return NotFound();
        }

        _context.HorariosDisponibles.Remove(horarioDisponible);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HorarioDisponibleExists(int id)
    {
        return _context.HorariosDisponibles.Any(e => e.IdHorario == id);
    }
}
