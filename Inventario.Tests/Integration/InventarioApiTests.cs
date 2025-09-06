using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Inventario.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Inventario.Api.Data;

namespace Inventario.Tests.Integration
{
    public class InventarioApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public InventarioApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Reemplazar la base de datos real con una en memoria para las pruebas
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<InventarioContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<InventarioContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });
                });
            });
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetProductos_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/inventario");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductoPorId_NonExistingId_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/inventario/999");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CrearProducto_ValidProducto_ReturnsCreatedResponse()
        {
            var nuevoProducto = new { Nombre = "Producto Test", Cantidad = 10, Precio = 100.0 };
            var response = await _client.PostAsJsonAsync("/api/inventario", nuevoProducto);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ActualizarProducto_NonExistingId_ReturnsNotFound()
        {
            var productoActualizado = new { Id = 999, Nombre = "Producto Actualizado", Cantidad = 20, Precio = 150.0 };
            var response = await _client.PutAsJsonAsync("/api/inventario/999", productoActualizado);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task EliminarProducto_NonExistingId_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync("/api/inventario/999");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}