using Microsoft.Extensions.Logging;
using PosFidelizacionAppV1._0.Services;
using PosFidelizacionAppV1._0.Pages;
using PosFidelizacionAppV1._0.ViewModels;

namespace PosFidelizacionAppV1._0
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Servicios
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<CustomerApiService>();
            builder.Services.AddSingleton<ProductApiService>();
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<UserService>();

            // ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<CustomersViewModel>();
            builder.Services.AddSingleton<ProductsViewModel>();
            builder.Services.AddSingleton<CartViewModel>();
            builder.Services.AddSingleton<SyncViewModel>();

            // Páginas
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<CustomersPage>();
            builder.Services.AddSingleton<ProductsPage>();
            builder.Services.AddSingleton<CartPage>();

            return builder.Build();
        }
    }
}
