using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using PosFidelizacionAppV1._0.Services;
using Microsoft.Maui.Controls;

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
            _userService = new UserService();
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