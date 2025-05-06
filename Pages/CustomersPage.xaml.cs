using Microsoft.Maui.Controls;
using PosFidelizacionAppV1._0.ViewModels;

namespace PosFidelizacionAppV1._0.Pages
{
    public partial class CustomersPage : ContentPage
    {
        private readonly CustomersViewModel _viewModel;

        public CustomersPage(CustomersViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
                await _viewModel.SyncCustomersAsync(); // sincroniza al aparecer
        }
    }
}
