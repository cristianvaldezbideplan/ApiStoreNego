using ApiStore.Data;
using ApiStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public UsuariosController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "AllUsuarios")]
        public async Task<IActionResult> AllUsuarios()
        {
            try
            {
                var lista = await _context.Usuarios.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{UsuarioId:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "UsuarioId")] int id)
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

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Usuarios usuarios)
        {
            try
            {
                await _context.Usuarios.AddAsync(usuarios);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{UsuarioId:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int UsuarioId)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(UsuarioId);

                if (usuarioExistente != null)
                {
                    _context.Usuarios.Remove(usuarioExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{UsuarioId:int}")]
        public async Task<IActionResult> Modificar([FromBody] Usuarios usuarios, [FromRoute] int UsuarioId)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(UsuarioId);

                if (usuarioExistente != null)
                {

                    if (!usuarios.Usuario.IsNullOrEmpty()) usuarioExistente.Usuario = usuarios.Usuario;
                    if (!usuarios.Password.IsNullOrEmpty()) usuarioExistente.Password = usuarios.Password;
                    if (!usuarios.Email.IsNullOrEmpty()) usuarioExistente.Email = usuarios.Email;
                    if (!usuarios.Domicilio.IsNullOrEmpty()) usuarioExistente.Domicilio = usuarios.Domicilio;
                    if (usuarios.IdRol != null) usuarioExistente.IdRol = usuarios.IdRol;

                    _context.Usuarios.Update(usuarioExistente);
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
