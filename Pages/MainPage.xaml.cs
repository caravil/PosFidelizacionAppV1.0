using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using PosFidelizacionAppV1._0.ViewModels;

namespace PosFidelizacionAppV1._0.Pages
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCerrarSesionClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        private async void OnProductosClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ProductsPage));
        }

        private async void OnClientesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CustomersPage));
        }

        private async void OnCartClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CartPage));
        }

    }
}

