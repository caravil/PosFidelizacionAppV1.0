using SQLite;

namespace PosFidelizacionAppV1._0.Models
{
    public class CartItem
    {

        public int ProductId { get; set; }
        public string Product { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Ignore]
        public decimal Subtotal => Price * Quantity;
    }
}
