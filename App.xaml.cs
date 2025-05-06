using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using PosFidelizacionAppV1._0.Services;
using PosFidelizacionAppV1._0.ViewModels;
using PosFidelizacionAppV1._0.Pages;

namespace PosFidelizacionAppV1._0
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
