using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PosFidelizacionAppV1._0.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace PosFidelizacionAppV1._0.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        private readonly UserService _userService;

        public LoginViewModel()
        {
            // Obtener la ruta del archivo de la base de datos
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");

            // Inicializar UserService con la ruta de la base de datos
            _userService = new UserService(dbPath);

            // Agregar el usuario predeterminado si no existe
            _userService.AddDefaultUserAsync().ConfigureAwait(false);
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    await Shell.Current.DisplayAlert("Error", "Debe ingresar email y contraseña.", "OK");
                    return;
                }

                var user = await _userService.ValidateCredentialsAsync(Email, Password);

                if (user != null)
                {
                    await Shell.Current.GoToAsync("MainPage");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Credenciales incorrectas.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
    }
}
