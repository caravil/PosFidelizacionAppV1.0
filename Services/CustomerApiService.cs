using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PosFidelizacionAppV1._0.Models;

namespace PosFidelizacionAppV1._0.Services
{
    public class CustomerApiService
    {
        private readonly HttpClient _httpClient;

        public CustomerApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                var apiCustomers = await _httpClient.GetFromJsonAsync<List<ApiCustomer>>("users");

                var customers = apiCustomers?.ConvertAll(api => new Customer
                {
                    Id = api.id,
                    Name = api.name,
                    Email = api.email,
                    LoyaltyPoints = new Random().Next(100, 1000) // Puntos simulados
                });

                return customers ?? new List<Customer>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[HTTP ERROR] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GENERAL ERROR] {ex.Message}");
            }

            return new List<Customer>();
        }

    }
}
