using ApiStore.Data;
using ApiStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiStore.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RegistroController : ControllerBase
{
    private readonly StoreDbContext _context;

    public RegistroController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "AllRegistro")]
    public async Task<IActionResult> AllRegistro()
    {
        try
        {
            var lista = await _context.Registro.ToListAsync();
            return Ok(lista);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ObtenerPorId/{registroId:int}")]
    public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "registroId")] int id)
    {
        try
        {
            var item = await _context.Registro.FindAsync(id);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Registro registro)
    {
        try
        {
            await _context.Registro.AddAsync(registro);
            var result = await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{registroId:int}")]
    public async Task<IActionResult> Borrar([FromRoute] int registroId)
    {
        try
        {
            var registroExistente = await _context.Registro.FindAsync(registroId);

            if (registroExistente != null)
            {
                _context.Registro.Remove(registroExistente);
                await _context.SaveChangesAsync();
            }


            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{registroId:int}")]
    public async Task<IActionResult> Modificar([FromBody] Registro registro, [FromRoute] int registroId)
    {
        try
        {
            var registroExistente = await _context.Registro.FindAsync(registroId);

            if (registroExistente != null)
            {
                if (!registro.Usuario.IsNullOrEmpty() ) registroExistente.Usuario = registro.Usuario;
                if (!registro.Password.IsNullOrEmpty()) registroExistente.Password = registro.Password;
                if (!registro.Email.IsNullOrEmpty()) registroExistente.Email = registro.Email;
                if (!registro.Domicilio.IsNullOrEmpty()) registroExistente.Domicilio = registro.Domicilio;
                if (!registro.Telefono.IsNullOrEmpty()) registroExistente.Telefono = registro.Telefono;
                if (!registro.Celular.IsNullOrEmpty()) registroExistente.Celular = registro.Celular;
                if (!registro.Provincia.IsNullOrEmpty()) registroExistente.Provincia = registro.Provincia;
                if (!registro.Localidad.IsNullOrEmpty()) registroExistente.Localidad = registro.Localidad;
                if (!registro.Password.IsNullOrEmpty()) registroExistente.Password = registro.Password;
                if (registro.CodigoPostal != null) registroExistente.CodigoPostal = registro.CodigoPostal;

                _context.Registro.Update(registroExistente);
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
