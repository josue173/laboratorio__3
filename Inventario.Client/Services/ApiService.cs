using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Inventario.Shared.Models;

namespace Inventario.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Producto>>("api/inventario") ?? new List<Producto>();
            }
            catch (HttpRequestException)
            {
                return new List<Producto>();
            }
        }

        public async Task<Producto?> GetProductoPorIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Producto>($"api/inventario/{id}");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<Producto?> CrearProductoAsync(Producto producto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/inventario", producto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Producto>();
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/inventario/{producto.Id}", producto);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/inventario/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}