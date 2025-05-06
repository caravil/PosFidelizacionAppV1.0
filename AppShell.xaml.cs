namespace PosFidelizacionAppV1._0
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(Pages.MainPage));
            Routing.RegisterRoute("LoginPage", typeof(Pages.LoginPage));
            Routing.RegisterRoute("CartPage", typeof(Pages.CartPage));
            Routing.RegisterRoute("CustomersPage", typeof(Pages.CustomersPage));
            Routing.RegisterRoute("ProductsPage", typeof(Pages.ProductsPage));
        }
    }
}
