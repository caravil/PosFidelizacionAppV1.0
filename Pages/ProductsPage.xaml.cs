using System;
using Microsoft.Maui.Controls;
using PosFidelizacionAppV1._0.ViewModels;

namespace PosFidelizacionAppV1._0.Pages
{
    public partial class ProductsPage : ContentPage
    {
        private readonly ProductsViewModel _viewModel;

        public ProductsPage(ProductsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadProductsAsync();
        }
    }
}
