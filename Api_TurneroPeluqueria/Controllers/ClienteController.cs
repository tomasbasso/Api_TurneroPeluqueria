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

    public class ClienteController : ControllerBase
        {
            private readonly TurneroDbContext _context;

            public ClienteController(TurneroDbContext context)
            {
                _context = context;
            }
            ////////BUSCAR TODOS//////////////////////////AUTOMAPER
            [HttpGet(Name = "ObtenerTodos")]
            public async Task<IActionResult> ObtenerTodos()
            {
                try
                {
                    var lista = await _context.Clientes.Select(u => new VerClientesDTO
                    {
                        Nombre = u.Nombre,
                        Apellido = u.Apellido,
                        Telefono = u.Telefono,
                        Email = u.Email,
                        FechaRegistro = u.Fecha_Registro
                    })
                        .ToListAsync();
                    return Ok(lista);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            ////////BUSCAR POR ID//////////////////////////
            [HttpGet("ObtenerPorId/{id:int}")]
            public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "id")] int id)
            {
                try
                {
                    var item = await _context.Clientes.FindAsync(id);
                    return Ok(item);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPost]
            public async Task<IActionResult> Crear(CrearClienteDTO clientedto)
            {
                try
                {
                    // Mapea el DTO a la entidad Usuario
                    var cliente = new Cliente
                    {

                        Nombre = clientedto.Nombre,
                        Apellido = clientedto.Apellido,
                        Telefono = clientedto.Telefono,
                        Email = clientedto.Email,
                        Contraseña = clientedto.Contraseña
                    };

                    await _context.Clientes.AddAsync(cliente);
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
            //////////BORRAR//////////
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> Borrar([FromRoute] int id)
            {
                try
                {
                    var ClienteExistente = await _context.Clientes.FindAsync(id);

                    if (ClienteExistente != null)
                    {
                        _context.Clientes.Remove(ClienteExistente);
                        await _context.SaveChangesAsync();
                    }


                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            [HttpPut("{id:int}")]
            public async Task<IActionResult> Modificar([FromBody] Cliente cliente, [FromRoute] int id)
            {
                try
                {
                    var ClienteExistente = await _context.Clientes.FindAsync(id);

                    if (ClienteExistente != null)
                    {
                        if (!cliente.Nombre.IsNullOrEmpty()) ClienteExistente.Nombre = cliente.Nombre;
                        if (!cliente.Apellido.IsNullOrEmpty()) ClienteExistente.Apellido = cliente.Apellido;
                        if (!cliente.Telefono.IsNullOrEmpty()) ClienteExistente.Telefono = cliente.Telefono;
                        if (!cliente.Email.IsNullOrEmpty()) ClienteExistente.Email = cliente.Email;
                        if (!cliente.Contraseña.IsNullOrEmpty()) ClienteExistente.Contraseña = cliente.Contraseña;


                        _context.Clientes.Update(ClienteExistente);
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
            public async Task<IActionResult> ValidarCredencial([FromBody] ClienteLoginDTO cliente)
            {
                var existeLogin = await _context.Clientes
                .AnyAsync(x => x.Email.Equals(cliente.Email) && x.Contraseña.Equals(cliente.Contraseña));

                Cliente clienteLogin = await _context.Clientes.FirstOrDefaultAsync(x => x.Email.Equals(cliente.Email) && x.Contraseña.Equals(cliente.Contraseña));

                if (existeLogin == null)
                {
                    // Si no se encuentra el usuario, retornar un mensaje de error
                    return NotFound("Usuario o contraseña incorrectos");
                }

            // Crear una respuesta con los datos del usuario autenticado
            ClienteLoginDTO ClienteResponse = new ClienteLoginDTO()
                {
                    Id = existeLogin ? clienteLogin.Id : 0,
                    Email = existeLogin ? clienteLogin.Email : "",
                    Contraseña = existeLogin ? clienteLogin.Contraseña : ""

                };

                // Retornar la respuesta con los datos del usuario
                return Ok(ClienteResponse);
            }
        }
    }

