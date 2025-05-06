using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosFidelizacionAppV1._0.Models;
using System.Collections.ObjectModel;
using PosFidelizacionAppV1._0.Services;
using System.Linq;

namespace PosFidelizacionAppV1._0.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public CartViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [ObservableProperty] 
        private Customer selectedCustomer;

        [ObservableProperty]
        private ObservableCollection<CartItem> cartItems = new();

        [ObservableProperty]
        private decimal totalAmount;

        public CartViewModel()
        {
            CartItems = new ObservableCollection<CartItem>();
            MessagingCenter.Subscribe<ProductsViewModel, Product>(this, "AddProductToCart", (sender, product) =>
            {
                AddToCart(product);
            });

            CalculateTotal();
        }

        public void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ProductsViewModel, Product>(this, "AddProductToCart");
        }

        [RelayCommand]
        public void AddToCart(Product product)
        {
            var existing = CartItems.FirstOrDefault(c => c.ProductId == product.ApiId);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    ProductId = product.ApiId,
                    Product = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            CalculateTotal();
        }

        public void UpdateQuantity(CartItem item, int newQuantity)
        {
            if (newQuantity > 0)
                item.Quantity = newQuantity;
            else
                CartItems.Remove(item);

            CalculateTotal();
        }

        [RelayCommand]
        public void IncreaseQuantity(CartItem item)
        {
            item.Quantity++;
            CalculateTotal();
        }

        [RelayCommand]
        public void DecreaseQuantity(CartItem item)
        {
            if (item.Quantity > 1)
                item.Quantity--;
            else
                CartItems.Remove(item);

            CalculateTotal();
        }
        [RelayCommand]
        public void RemoveFromCart(CartItem item)
        {
            CartItems.Remove(item);
            CalculateTotal();
        }

        [RelayCommand]
        public async Task FinalizePurchaseAsync()
        {
            if (CartItems.Count == 0)
                return;

            var sale = new Sale
            {
                ClientId = 1, // Puedes adaptar esto cuando permitas seleccionar el cliente
                SaleDate = DateTime.Now,
                TotalAmount = TotalAmount,
                LoyaltyPointsUsed = 0, // O lo que corresponda
                LoyaltyPointsEarned = (int)(TotalAmount / 10), // Ejemplo simple
                PaymentMethod = "Efectivo" // O podrías dejar que el usuario elija
            };

            var db = new DatabaseService();
            int saleId = await db.AddSaleAsync(sale);

            var saleDetails = CartItems.Select(item => new SaleDetail
            {
                SaleId = saleId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();

            await db.AddSaleDetailsAsync(saleDetails);

            CartItems.Clear();
            CalculateTotal();

            MessagingCenter.Send(this, "PurchaseCompleted", "La compra ha sido finalizada.");
        }

        private void CalculateTotal()
        {
            TotalAmount = CartItems.Sum(c => c.Subtotal);
        }

        public async Task<bool> ProcesarVentaAsync()
        {
            if (SelectedCustomer == null || CartItems.Count == 0)
                return false;

            var total = CartItems.Sum(p => p.Price * p.Quantity);
            var puntosGanados = (int)(total / 10); // Ejemplo: 1 punto por cada $10

            var nuevaVenta = new Sale
            {
                ClientId = SelectedCustomer.Id,
                SaleDate = DateTime.Now,
                TotalAmount = total,
                LoyaltyPointsEarned = puntosGanados,
                LoyaltyPointsUsed = 0,
                PaymentMethod = "Efectivo" // Por ahora estático
            };

            await _databaseService.AddSaleAsync(nuevaVenta);

            var detalles = CartItems.Select(item => new SaleDetail
            {
                SaleId = nuevaVenta.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();

            await _databaseService.AddSaleDetailsAsync(detalles);

            // Actualizar puntos del cliente
            SelectedCustomer.LoyaltyPoints += puntosGanados;
            await _databaseService.UpdateCustomerAsync(SelectedCustomer);

            // Limpiar carrito
            CartItems.Clear();

            return true;
        }

    }
}
