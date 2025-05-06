using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using PosFidelizacionAppV1._0.Models;

namespace PosFidelizacionAppV1._0.Services
{
    public class UserService
    {
        private readonly SQLiteAsyncConnection _database;

        public UserService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait(); 
        }

        // Agregar credenciales usando SQLite
        public async Task AddDefaultUserAsync()
        {
            var existingUser = await _database.Table<User>().FirstOrDefaultAsync();
            if (existingUser == null) // Si no hay usuarios en la base de datos
            {
                var user = new User
                {
                    Email = "admin",
                    PasswordHash = "1234" // Asegúrate de cifrar la contraseña en una implementación real
                };
                await _database.InsertAsync(user);
            }
        }

        // Validar credenciales usando SQLite
        public async Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _database.Table<User>()
                .Where(u => u.Email == email && u.PasswordHash == password)
                .FirstOrDefaultAsync();
            return user;
        }


    }
}
