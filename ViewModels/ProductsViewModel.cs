using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosFidelizacionAppV1._0.Models;
using PosFidelizacionAppV1._0.Services;
using PosFidelizacionAppV1._0.Pages;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PosFidelizacionAppV1._0.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly ProductApiService _productApiService;
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string statusMessage;

        [ObservableProperty]
        public ObservableCollection<Product> products;

        // Propiedades para ActivityIndicator
        [ObservableProperty]
        private bool isRunning;

        [ObservableProperty]
        private bool isVisible;

        public ProductsViewModel(ProductApiService productApiService, DatabaseService databaseService)
        {
            _productApiService = productApiService;
            _databaseService = databaseService;
            Products = new ObservableCollection<Product>();
            IsRunning = false;
            IsVisible = false;
        }

        [RelayCommand]
        public async Task SyncProductsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            IsRunning = true;  // Activar el ActivityIndicator
            IsVisible = true;

            StatusMessage = "Sincronizando productos...";

            var products = await _productApiService.GetProductsFromApiAsync();
            foreach (var product in products)
            {
                await _databaseService.AddProductAsync(product);
            }

            StatusMessage = "Productos sincronizados.";
            await LoadProductsAsync();

            IsRunning = false;  // Desactivar el ActivityIndicator
            IsVisible = false;
            IsBusy = false;
        }

        [RelayCommand]
        void AddToCart(Product product)
        {
            MessagingCenter.Send(this, "AddProductToCart", product);
        }

        [RelayCommand]
        public async Task NavigateToCartAsync()
        {
            try
            {
                Console.WriteLine("Navegando al carrito...");
                await Shell.Current.GoToAsync(nameof(CartPage));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al navegar al carrito: {ex.Message}");
            }
        }

        public async Task LoadProductsAsync()
        {
            try
            {
                var productList = await _databaseService.GetProductsAsync();
                Products.Clear();
                foreach (var product in productList)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar productos: {ex.Message}");
            }
        }
    }
}

