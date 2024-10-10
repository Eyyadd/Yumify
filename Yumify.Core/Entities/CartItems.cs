namespace Yumify.Core.Entities
{
    public class CartItems
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
    }
}