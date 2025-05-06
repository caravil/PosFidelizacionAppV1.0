using SQLite;

namespace PosFidelizacionAppV1._0.Models
{
    public class CartItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Ignore]
        public decimal Subtotal => Price * Quantity;
    }
}
