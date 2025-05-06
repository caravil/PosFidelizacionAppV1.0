using System;
using PosFidelizacionAppV1._0.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosFidelizacionAppV1._0.Services
{
    public class UserService
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 1, Email = "admin@pos.com", Password = "1234" }
        };

        public Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            var user = _users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return Task.FromResult(user);
        }
    }
}
