using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosFidelizacionAppV1._0.Models;
using PosFidelizacionAppV1._0.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PosFidelizacionAppV1._0.ViewModels
{
    public partial class SyncViewModel : ObservableObject
    {
        private readonly CustomerApiService _customerApiService;
        private readonly ProductApiService _productApiService;
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string statusMessage;

        public SyncViewModel(
            CustomerApiService customerApiService,
            ProductApiService productApiService,
            DatabaseService databaseService)
        {
            _customerApiService = customerApiService;
            _productApiService = productApiService;
            _databaseService = databaseService;
        }

        [RelayCommand]
        public async Task SyncDataAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            StatusMessage = "Sincronizando datos...";

            // 1. Obtener y guardar Customers
            var customers = await _customerApiService.GetCustomersAsync();
            Console.WriteLine($"Número de clientes recibidos: {customers.Count}");
            foreach (var customer in customers)
            {
                // Verificar si el cliente ya existe
                var existingCustomer = (await _databaseService.GetCustomersAsync()).FirstOrDefault(c => c.Id == customer.Id);
                if (existingCustomer == null)
                {
                    // Si no existe, lo insertamos
                    await _databaseService.AddCustomerAsync(customer);
                }
                else
                {
                    // Si existe, actualizamos si es necesario
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Email = customer.Email;
                    await _databaseService.UpdateCustomerAsync(existingCustomer); // Asumiendo que tienes un método de actualización
                }
            }

            // 2. Obtener y guardar Products
            var products = await _productApiService.GetProductsFromApiAsync();
            Console.WriteLine($"Número de productos recibidos: {products.Count}");
            foreach (var product in products)
            {
                Console.WriteLine($"Producto: {product.Name}, Precio: {product.Price}");
                // Verificar si el producto ya existe
                var existingProduct = (await _databaseService.GetProductsAsync()).FirstOrDefault(p => p.ApiId == product.ApiId);
                if (existingProduct == null)
                {
                    // Si no existe, lo insertamos
                    await _databaseService.AddProductAsync(product);
                }
                else
                {
                    // Si existe, actualizamos si es necesario
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.Description = product.Description;
                    await _databaseService.UpdateProductAsync(existingProduct); // Asumiendo que tienes un método de actualización
                }
            }

            StatusMessage = "Sincronización completada.";
            IsBusy = false;
        }
    }
}
