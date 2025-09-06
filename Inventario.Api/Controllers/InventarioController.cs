using Microsoft.AspNetCore.Mvc;
using Inventario.Api.Models;
using Inventario.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioService _inventarioService;

        public InventarioController(InventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _inventarioService.ObtenerProductos();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProductoPorId(int id)
        {
            var producto = await _inventarioService.ObtenerProductoPorId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProducto(Producto producto)
        {
            var nuevoProducto = await _inventarioService.AgregarProducto(producto);
            return CreatedAtAction(nameof(GetProductoPorId), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            await _inventarioService.ActualizarProducto(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _inventarioService.ObtenerProductoPorId(id);
            if (producto == null)
            {
                return NotFound();
            }

            await _inventarioService.EliminarProducto(id);
            return NoContent();
        }
    }
}