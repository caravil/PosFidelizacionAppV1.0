using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _viewModel = (ProductsViewModel)BindingContext;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.SyncProductsAsync(); 
        }
    }
}

