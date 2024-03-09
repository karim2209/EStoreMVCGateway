using Microsoft.AspNetCore.Mvc;

namespace EStoreMVCGateway.Controllers
{
    public class ProductItemController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductItemRepository _productItemRepository;

        public ProductItemController(ILogger<HomeController> logger, IProductItemRepository productItemRepository)
        {
            _logger = logger;
            _productItemRepository = productItemRepository;            
        }

        public async Task<IActionResult> Index(string sterm = "", int categoryId = 0)
        {
            IEnumerable<Product> products = await _productItemRepository.GetItemProduct(categoryId);
            IEnumerable<Category> categories = await _productItemRepository.Categories();
            ProductItemDisplayModel productItemModel = new ProductItemDisplayModel
            {
                Products = products,
                Categories = categories,
                CategoryId = categoryId
            };
            return View(productItemModel);
        }
    }
}
