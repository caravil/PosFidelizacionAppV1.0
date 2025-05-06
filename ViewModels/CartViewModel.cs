using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosFidelizacionAppV1._0.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PosFidelizacionAppV1._0.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
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

            // Llama a CalculateTotal después de la suscripción inicial
            CalculateTotal();
        }

        // Recuerda cancelar la suscripción cuando ya no sea necesario
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
                    ProductName = product.Name,
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
        public void FinalizePurchase()
        {
            MessagingCenter.Send(this, "PurchaseCompleted", "La compra ha sido finalizada.");

            // Limpiar el carrito y resetear el total
            CartItems.Clear();
            TotalAmount = 0;
        }

        private void CalculateTotal()
        {
            TotalAmount = CartItems.Sum(c => c.Subtotal);
        }
    }
}
