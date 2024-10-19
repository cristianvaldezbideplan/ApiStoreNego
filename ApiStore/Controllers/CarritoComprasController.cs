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
    public class CarritoComprasController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public CarritoComprasController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "AllCarritoCompra")]
        public async Task<IActionResult> AllCarritoCompra()
        {
            try
            {
                var lista = await _context.CarritoCompra.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{CarritoCompraId:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "CarritoCompraId")] int id)
        {
            try
            {
                var item = await _context.CarritoCompra.FindAsync(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CarritoCompra carritocompra)
        {
            try
            {
                await _context.CarritoCompra.AddAsync(carritocompra);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{CarritoCompraId:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int CarritoCompraId)
        {
            try
            {
                var CarritoCompraExistente = await _context.CarritoCompra.FindAsync(CarritoCompraId);

                if (CarritoCompraExistente != null)
                {
                    _context.CarritoCompra.Remove(CarritoCompraExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{CarritoCompraId:int}")]
        public async Task<IActionResult> Modificar([FromBody] CarritoCompra carritocompra, [FromRoute] int CarritoCompraId)
        {
            try
            {
                var CarritoCompraExistente = await _context.CarritoCompra.FindAsync(CarritoCompraId);

                if (CarritoCompraExistente != null)
                {
                    if (carritocompra.CostoTotal != null) CarritoCompraExistente.CostoTotal = carritocompra.CostoTotal;
                    if (carritocompra.Cantidad != null) CarritoCompraExistente.Cantidad = carritocompra.Cantidad;
                    if (!carritocompra.Descripcion.IsNullOrEmpty()) CarritoCompraExistente.Descripcion = carritocompra.Descripcion;
                    carritocompra.Fecha = DateTime.Now;   

                    _context.CarritoCompra.Update(CarritoCompraExistente);
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
