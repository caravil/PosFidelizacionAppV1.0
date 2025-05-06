using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosFidelizacionAppV1._0.Services;
using PosFidelizacionAppV1._0.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PosFidelizacionAppV1._0.ViewModels;

namespace PosFidelizacionAppV1._0.ViewModels
{
    public partial class CustomersViewModel : ObservableObject
    {
        private readonly CustomerApiService _customerApiService;
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string statusMessage;

        [ObservableProperty]
        public ObservableCollection<Customer> customers;

        // Inyección de las dependencias
        public CustomersViewModel(CustomerApiService customerApiService, DatabaseService databaseService)
        {
            _customerApiService = customerApiService;
            _databaseService = databaseService;
            Customers = new ObservableCollection<Customer>();
        }

        [RelayCommand]
        public async Task SyncCustomersAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            StatusMessage = "Sincronizando clientes...";

            // Obtener y guardar los clientes
            var customers = await _customerApiService.GetCustomersAsync();
            foreach (var customer in customers)
            {
                await _databaseService.AddCustomerAsync(customer);  // Usar el servicio inyectado
            }

            StatusMessage = "Clientes sincronizados.";

            // Cargar los clientes desde la base de datos
            await LoadCustomersAsync();
            IsBusy = false;
        }

        public async Task LoadCustomersAsync()
        {
            var customerList = await _databaseService.GetCustomersAsync(); // Usar el servicio inyectado
            Customers.Clear();
            foreach (var customer in customerList)
            {
                Customers.Add(customer);
            }
        }
    }
}
