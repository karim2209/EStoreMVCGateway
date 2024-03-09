using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EStoreMVCGateway.Repositories
{
    public class ProductItemRepository : IProductItemRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public ProductItemRepository(ApplicationDbContext db, IHttpContextAccessor httpcontextAccessor,
                                UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpcontextAccessor = httpcontextAccessor;
        }
        public async Task<IEnumerable<Category>> Categories()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetItemProduct(int categoryId = 0)
        {
            IEnumerable<Product> products = await (from product in _db.Products
                                                   join category in _db.Categories
                                                   on product.CategoryId equals category.CategoryId
                                                   select new Product
                                                   {
                                                       ProductId = product.ProductId,
                                                       Image = product.Image,
                                                       ProductName = product.ProductName,
                                                       ProductDescription = product.ProductDescription,
                                                       Rating = product.Rating,
                                                       CategoryId = product.CategoryId,
                                                       Price = product.Price,
                                                       CategoryName = product.Category.CategoryName
                                                   }
                               ).ToListAsync();
            if (categoryId > 0)
            {
                products = products.Where(a => a.CategoryId == categoryId).ToList();
            }
            return products;
        }
    }
}
