﻿using Microsoft.Extensions.Logging;
using PosFidelizacionAppV1._0.Services;
using PosFidelizacionAppV1._0.Pages;
using PosFidelizacionAppV1._0.ViewModels;
using Microsoft.Extensions.DependencyInjection;


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

            // Ruta de la base de datos
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "local.db");

            // Servicios internos
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<UserService>();

            // Registrar UserService con la ruta de la base de datos
            builder.Services.AddSingleton<UserService>(s => new UserService(dbPath));

            // Servicios API con HttpClient configurado por servicio
            builder.Services.AddHttpClient<ProductApiService>(client =>
            {
                client.BaseAddress = new Uri("https://fakestoreapi.com");
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            builder.Services.AddHttpClient<CustomerApiService>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                client.Timeout = TimeSpan.FromSeconds(30);
            });

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
