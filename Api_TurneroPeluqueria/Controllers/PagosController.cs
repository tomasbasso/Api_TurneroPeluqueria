using Api_TurneroPeluqueria.Data;
using Api_TurneroPeluqueria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PagosController : ControllerBase
{
    private readonly TurneroDbContext _context;

    public PagosController(TurneroDbContext context)
    {
        _context = context;
    }

    // GET: api/Pagos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
    {
        return await _context.Pagos.Include(p => p.Usuario).Include(p => p.Turno).ToListAsync();
    }

    // GET: api/Pagos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pago>> GetPago(int id)
    {
        var pago = await _context.Pagos
            .Include(p => p.Usuario)
            .Include(p => p.Turno)
            .FirstOrDefaultAsync(p => p.IdPago == id);

        if (pago == null)
        {
            return NotFound();
        }

        return pago;
    }

    // GET: api/Pagos/Usuario/5
    [HttpGet("Usuario/{idUsuario}")]
    public async Task<ActionResult<IEnumerable<Pago>>> GetPagosPorUsuario(int idUsuario)
    {
        var pagos = await _context.Pagos
            .Where(p => p.IdUsuario == idUsuario)
            .Include(p => p.Turno)
            .ToListAsync();

        return Ok(pagos);
    }

    // POST: api/Pagos
    [HttpPost]
    public async Task<ActionResult<Pago>> PostPago(Pago pago)
    {
        // Validar que el turno esté reservado por el usuario
        var turno = await _context.Turnos.FindAsync(pago.IdTurno);
        if (turno == null || turno.IdUsuario != pago.IdUsuario)
        {
            return BadRequest("El turno no está reservado por el usuario especificado.");
        }

        // Agregar el pago
        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPago", new { id = pago.IdPago }, pago);
    }

    // PUT: api/Pagos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPago(int id, Pago pago)
    {
        if (id != pago.IdPago)
        {
            return BadRequest();
        }

        _context.Entry(pago).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PagoExists(id))
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

    // DELETE: api/Pagos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePago(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);
        if (pago == null)
        {
            return NotFound();
        }

        _context.Pagos.Remove(pago);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PagoExists(int id)
    {
        return _context.Pagos.Any(e => e.IdPago == id);
    }
}
