using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PosFidelizacionAppV1._0.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; 
    }
}


