using EStoreMVCGateway.Models;

namespace EStoreMVCGateway
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Category>> Categories();
        Task<IEnumerable<Product>> GetProducts(string sTerm = "", int categoryid = 0);
    }
}