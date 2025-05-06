using System;
using Microsoft.Maui.Controls;
using PosFidelizacionAppV1._0.Models;
using PosFidelizacionAppV1._0.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosFidelizacionAppV1._0.Services;
using PosFidelizacionAppV1._0.Pages;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PosFidelizacionAppV1._0.Pages
{
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            BindingContext = new CartViewModel(); // Establecer el ViewModel a CartViewModel
        }

        private void OnQuantityChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry &&
                entry.BindingContext is CartItem item &&
                int.TryParse(e.NewTextValue, out int newQty))
            {
                // Obtener CartViewModel directamente
                var cartVM = BindingContext as CartViewModel;
                cartVM?.UpdateQuantity(item, newQty);
            }
        }

        private void OnRemoveClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is CartItem item)
            {
                // Obtener CartViewModel directamente
                var cartVM = BindingContext as CartViewModel;
                cartVM?.RemoveFromCart(item);
            }
        }
    }
}
