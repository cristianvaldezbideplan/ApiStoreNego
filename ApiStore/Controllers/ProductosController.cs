using ApiStore.Data;
using ApiStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly StoreDbContext _context;

    public ProductosController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "ObtenerTodos")]
    public async Task<IActionResult> ObtenerTodos()
    {
        try
        {
            var lista = await _context.Productos.ToListAsync();
            return Ok(lista);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ObtenerPorId/{productoId:int}")]
    public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "productoId")] int id)
    {
        try
        {
            var item = await _context.Productos.FindAsync(id);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Producto producto)
    {
        try
        {
            await _context.Productos.AddAsync(producto);
            var result = await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{productoId:int}")]
    public async Task<IActionResult> Borrar([FromRoute] int productoId)
    {
        try
        {
            var productoExistente = await _context.Productos.FindAsync(productoId);

            if(productoExistente != null)
            {
                _context.Productos.Remove(productoExistente);
                await _context.SaveChangesAsync();
            }
                

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{productoId:int}")]
    public async Task<IActionResult> Modificar([FromBody] Producto producto, [FromRoute] int productoId)
    {
        try
        {
            var productoExistente = await _context.Productos.FindAsync(productoId);

            if (productoExistente != null)
            {
                if (!producto.NumeroProducto.IsNullOrEmpty()) productoExistente.NumeroProducto = producto.NumeroProducto;
                if (!producto.NombreProducto.IsNullOrEmpty()) productoExistente.NombreProducto = producto.NombreProducto;
                if (!producto.Medidas.IsNullOrEmpty()) productoExistente.Medidas = producto.Medidas;
                if (producto.Cantidad != null) productoExistente.Cantidad = producto.Cantidad;
                if (producto.Peso != null) productoExistente.Peso = producto.Peso;
                if (producto.Precio != null) productoExistente.Precio = producto.Precio;
                if (!producto.TipoDeMoneda.IsNullOrEmpty()) productoExistente.TipoDeMoneda = producto.TipoDeMoneda;
                if (!producto.Descripcion.IsNullOrEmpty()) productoExistente.Descripcion = producto.Descripcion;
                if (producto.Stock != null) productoExistente.Stock = producto.Stock;
                if (!producto.Imagen.IsNullOrEmpty()) productoExistente.Imagen = producto.Imagen;
                if (!producto.RutaImagen.IsNullOrEmpty()) productoExistente.RutaImagen = producto.RutaImagen;
                if (!producto.RutaImagenLocal.IsNullOrEmpty()) productoExistente.RutaImagenLocal = producto.RutaImagenLocal;

                _context.Productos.Update(productoExistente);
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
