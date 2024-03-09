namespace EStoreMVCGateway.Models.DTO
{
    public class ProductItemDisplayModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int CategoryId { get; set; } = 0;
    }
}
