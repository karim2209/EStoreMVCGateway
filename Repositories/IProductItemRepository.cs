namespace EStoreMVCGateway
{
    public interface IProductItemRepository
    {
        Task<IEnumerable<Category>> Categories();
        Task<IEnumerable<Product>> GetItemProduct(int categoryId = 0);
    }
}