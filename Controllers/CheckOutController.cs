using EStoreMVCGateway.Models;
using EStoreMVCGateway.Repositories;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;


namespace EStoreMVCGateway.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ICheckOutRepository _checkOutRepository;
        private readonly SessionService _sessionService;
        public CheckOutController(ICheckOutRepository checkOutRepository, SessionService sessionService)
        {
            _checkOutRepository = checkOutRepository;
            _sessionService = sessionService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var shoppingCart = await _checkOutRepository.GetUserCart();
                return View(shoppingCart);
            }
            catch (Exception ex)
            {
                // Log the exception and redirect to an error view
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult OrderConfirmation()
        {
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "paid")
            {
                var transaction = session.PaymentIntentId.ToString();
                return View("Success");
            }
            return View("Login");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> CheckOut()
        {
            var shoppingCart = await _checkOutRepository.GetUserCart();
            var cartDetails = shoppingCart?.CartDetails.ToList() ?? new List<CartDetail>();
            var domain = "https://localhost:7149/";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain + $"CheckOut/OrderConfirmation",
                CancelUrl = domain + $"CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"

            };
            foreach (var item in cartDetails)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * item.Quantity),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.ProductName,
                        }
                    },
                    Quantity = item.Quantity,
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);
            TempData["Session"] = session.Id;
            

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
