using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PosFidelizacionAppV1._0.Models
{
    public class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int LoyaltyPointsUsed { get; set; }
        public int LoyaltyPointsEarned { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
