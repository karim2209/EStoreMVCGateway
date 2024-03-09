namespace EStoreMVCGateway.Models
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public long Price { get; set; }
        public long Quantity { get; set; }
        public string Image { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

    }
}
