using PosFidelizacionAppV1._0.ViewModels;

namespace PosFidelizacionAppV1._0.Pages
{
    public partial class CustomersPage : ContentPage
    {
        private CustomersViewModel ViewModel => BindingContext as CustomersViewModel;

        public CustomersPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
                await ViewModel.SyncCustomersAsync(); // sincroniza al aparecer
        }
    }
}
