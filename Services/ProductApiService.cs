using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PosFidelizacionAppV1._0.Models;

namespace PosFidelizacionAppV1._0.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsFromApiAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://fakestoreapi.com/products");

                response.EnsureSuccessStatusCode(); // Lanza excepción si no es 2xx

                var json = await response.Content.ReadAsStringAsync();

                var products = JsonSerializer.Deserialize<List<Product>>(json);

                if (products == null)
                    return new List<Product>();

                foreach (var product in products)
                {
                    product.Rate = product.Rating?.Rate ?? 0;
                    product.Count = product.Rating?.Count ?? 0;
                }

                return products;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[HTTP ERROR] {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[I/O ERROR] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GENERAL ERROR] {ex.Message}");
            }

            return new List<Product>();
        }

    }
}
