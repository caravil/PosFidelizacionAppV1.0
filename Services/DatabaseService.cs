using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PosFidelizacionAppV1._0.Models;

namespace PosFidelizacionAppV1._0.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        private bool _initialized = false;

        // Usamos Lazy para inicializar solo cuando sea necesario
        private readonly Lazy<Task> InitTask;

        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "POSFidelizacion.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Inicialización diferida para evitar bloqueos
            InitTask = new Lazy<Task>(async () => await InitAsync());
        }

        private async Task InitAsync()
        {
            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<Customer>();
            await _database.CreateTableAsync<Product>();
            await _database.CreateTableAsync<Sale>();
            await _database.CreateTableAsync<SaleDetail>();

            _initialized = true;
        }

        private async Task EnsureInitializedAsync()
        {
            if (!_initialized)
            {
                await InitTask.Value;
            }
        }

        // Métodos para manejar Clientes
        public async Task<List<Customer>> GetCustomersAsync()
        {
            await EnsureInitializedAsync();
            return await _database.Table<Customer>().ToListAsync();
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            await EnsureInitializedAsync();
            return await _database.InsertOrReplaceAsync(customer);
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            await EnsureInitializedAsync();
            return await _database.UpdateAsync(customer);
        }

        public async Task AddAllCustomersAsync(IEnumerable<Customer> customers)
        {
            await EnsureInitializedAsync();
            await _database.InsertAllAsync(customers, "OR REPLACE");
        }

        // Métodos para manejar Productos
        public async Task<List<Product>> GetProductsAsync()
        {
            await EnsureInitializedAsync();
            return await _database.Table<Product>().ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await EnsureInitializedAsync();
            var existing = await _database.Table<Product>().FirstOrDefaultAsync(p => p.ApiId == product.ApiId);
            if (existing == null)
            {
                await _database.InsertAsync(product);
            }
            else
            {
                existing.Name = product.Name;
                existing.Description = product.Description;
                existing.Price = product.Price;
                existing.ImageUrl = product.ImageUrl;
                existing.Category = product.Category;
                await _database.UpdateAsync(existing);
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            await EnsureInitializedAsync();
            return await _database.UpdateAsync(product);
        }

        // Métodos para manejar Ventas
        public async Task<List<Sale>> GetSalesAsync()
        {
            await EnsureInitializedAsync();
            return await _database.Table<Sale>().ToListAsync();
        }

        public async Task<int> AddSaleAsync(Sale sale)
        {
            await EnsureInitializedAsync();
            return await _database.InsertAsync(sale);
        }

        // Métodos para manejar Detalles de Venta
        public async Task<List<SaleDetail>> GetSaleDetailsAsync()
        {
            await EnsureInitializedAsync();
            return await _database.Table<SaleDetail>().ToListAsync();
        }

        public async Task<int> AddSaleDetailAsync(SaleDetail saleDetail)
        {
            await EnsureInitializedAsync();
            return await _database.InsertAsync(saleDetail);
        }

        public async Task AddSaleDetailsAsync(IEnumerable<SaleDetail> saleDetails)
        {
            await EnsureInitializedAsync();
            await _database.InsertAllAsync(saleDetails);
        }

    }
}
