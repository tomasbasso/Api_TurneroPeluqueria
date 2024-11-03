using Api_TurneroPeluqueria.Data;
using Api_TurneroPeluqueria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RolController : ControllerBase
{
    private readonly TurneroDbContext _context;

    public RolController(TurneroDbContext context)
    {
        _context = context;
    }

    // GET: api/Roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    // GET: api/Roles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Rol>> GetRol(int id)
    {
        var rol = await _context.Roles.FindAsync(id);

        if (rol == null)
        {
            return NotFound();
        }

        return rol;
    }

    // POST: api/Roles
    [HttpPost]
    public async Task<ActionResult<Rol>> PostRol(Rol rol)
    {
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRol", new { id = rol.IdRol }, rol);
    }

    // PUT: api/Roles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRol(int id, Rol rol)
    {
        if (id != rol.IdRol)
        {
            return BadRequest();
        }

        _context.Entry(rol).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RolExists(id))
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

    // DELETE: api/Roles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRol(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol == null)
        {
            return NotFound();
        }

        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RolExists(int id)
    {
        return _context.Roles.Any(e => e.IdRol == id);
    }
}
