using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Services
{
    public class InventarioService
    {
        private readonly InventarioContext _context;

        public InventarioService(InventarioContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<Producto> AgregarProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto> ActualizarProducto(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return false;
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}