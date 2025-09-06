using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Inventario.Shared.Models;
using Inventario.Api.Services;
using Inventario.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Tests.Unit
{
    [TestClass]
    public class InventarioServiceTests
    {
        private InventarioService _inventarioService;
        private Mock<InventarioContext> _mockContext;
        private Mock<DbSet<Producto>> _mockDbSet;

        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<InventarioContext>(new DbContextOptions<InventarioContext>());
            _mockDbSet = new Mock<DbSet<Producto>>();
            _inventarioService = new InventarioService(_mockContext.Object);
        }

        [TestMethod]
        public async Task ObtenerProductos_ReturnsAllProducts()
        {
            // Arrange
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Producto 1", Cantidad = 10, Precio = 100 },
                new Producto { Id = 2, Nombre = "Producto 2", Cantidad = 20, Precio = 200 }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<Producto>>().Setup(m => m.Provider).Returns(productos.Provider);
            _mockDbSet.As<IQueryable<Producto>>().Setup(m => m.Expression).Returns(productos.Expression);
            _mockDbSet.As<IQueryable<Producto>>().Setup(m => m.ElementType).Returns(productos.ElementType);
            _mockDbSet.As<IQueryable<Producto>>().Setup(m => m.GetEnumerator()).Returns(productos.GetEnumerator());

            _mockContext.Setup(c => c.Productos).Returns(_mockDbSet.Object);

            // Act
            var result = await _inventarioService.ObtenerProductos();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task ObtenerProductoPorId_ExistingId_ReturnsProduct()
        {
            // Arrange
            var producto = new Producto { Id = 1, Nombre = "Producto 1", Cantidad = 10, Precio = 100 };
            _mockDbSet.Setup(x => x.FindAsync(1)).ReturnsAsync(producto);
            _mockContext.Setup(c => c.Productos).Returns(_mockDbSet.Object);

            // Act
            var result = await _inventarioService.ObtenerProductoPorId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Producto 1", result.Nombre);
        }

        [TestMethod]
        public async Task AgregarProducto_ValidProduct_AddsProduct()
        {
            // Arrange
            var producto = new Producto { Id = 3, Nombre = "Producto 3", Cantidad = 30, Precio = 300 };
            _mockContext.Setup(c => c.Productos).Returns(_mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _inventarioService.AgregarProducto(producto);

            // Assert
            _mockDbSet.Verify(x => x.Add(producto), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task ActualizarProducto_ExistingProduct_UpdatesProduct()
        {
            // Arrange
            var producto = new Producto { Id = 1, Nombre = "Producto 1", Cantidad = 10, Precio = 100 };
            _mockContext.Setup(c => c.Productos).Returns(_mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _inventarioService.ActualizarProducto(producto);

            // Assert
            _mockContext.Verify(x => x.Entry(producto), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task EliminarProducto_ExistingId_RemovesProduct()
        {
            // Arrange
            var producto = new Producto { Id = 1, Nombre = "Producto 1", Cantidad = 10, Precio = 100 };
            _mockDbSet.Setup(x => x.FindAsync(1)).ReturnsAsync(producto);
            _mockContext.Setup(c => c.Productos).Returns(_mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _inventarioService.EliminarProducto(1);

            // Assert
            Assert.IsTrue(result);
            _mockDbSet.Verify(x => x.Remove(producto), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}