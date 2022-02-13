namespace FarmersMarket.Web.Controllers
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using FarmersMarket.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using Stripe.Checkout;
    using System.Collections.Generic;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService service;
        private readonly IProfileService userService;
        private readonly IConfiguration configuration;

        public ShoppingCartController(IShoppingCartService service, IProfileService userService, IConfiguration configuration)
        {
            this.service = service;
            this.userService = userService;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!service.IsProfileComplete(this.userService.GetCurrentUser().Result))
            {
                TempData["controllerName"] = this.ControllerContext.RouteData.Values["controller"]?.ToString();
                TempData["InfoMessage"] = "Please fill out all your profile information before placing an order.";
                return this.RedirectToAction("Edit", "Profile");
            }

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return View(carts);
        }

        [HttpGet]
        [Route("/shoppingcart/addproduct/{id}")]
        public IActionResult AddProduct(int id)
        {
            Models.EntityModels.Product? product = service.GetProduct(id);

            ShoppingCart cart = service.GetShoppingCart(this.userService.GetCurrentUser().Result);

            if (product != null)
            {
                service.AddToShoppingCart(cart, product);
                this.TempData["SuccessMessage"] = MessagesConstants.AddProductToCartSuccessMessage;
            }

            return this.RedirectToAction("All", "Products");
        }

        [HttpGet]
        [Route("/shoppingcart/cart/{cartId}/removeproduct/{id}")]
        public IActionResult RemoveProduct(int cartId, int id)
        {
            Models.EntityModels.Product? product = service.GetProduct(id);

            ShoppingCart? cart = service.GetCurrentShoppingCart(cartId);

            if (product != null && cart != null)
            {
                service.RemoveFromShoppingCart(cart, product);
                this.TempData["SuccessMessage"] = MessagesConstants.RemoveProductFromCartSuccessMessage;
            }

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("/shoppingcart/cart/{cartId}/decreaseproductunits/{id}")]
        public IActionResult DecreaseProductUnits(int cartId, int id)
        {
            Models.EntityModels.Product? product = service.GetProduct(id);

            ShoppingCart? cart = service.GetCurrentShoppingCart(cartId);

            if (product != null && cart != null)
            {
                service.DecreaseProductUnitsFromShoppingCart(cart, product);
            }

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("/shoppingcart/cart/{cartId}/increaseproductunits/{id}")]
        public IActionResult IncreaseProductUnits(int cartId, int id)
        {
            Models.EntityModels.Product? product = service.GetProduct(id);

            ShoppingCart? cart = service.GetCurrentShoppingCart(cartId);

            if (product != null && cart != null)
            {
                service.IncreaseProductUnitsFromShoppingCart(cart, product);
            }

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("/shoppingcart/placeorder/{id}")]
        public IActionResult PlaceOrder(int id, decimal totalAmount)
        {
            if (!service.IsProfileComplete(this.userService.GetCurrentUser().Result))
            {
                UserViewModel viewModel = userService.GetProfile();
                return this.PartialView("_EditProfilePartial", viewModel);
            }

            service.MakeAnOrder(id, totalAmount);

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpPost]
        public IActionResult PlaceOrderWithStripe(string stripeToken, string stripeEmail, decimal total, int cartId)
        {
            if (!service.IsProfileComplete(this.userService.GetCurrentUser().Result))
            {
                UserViewModel viewModel = userService.GetProfile();
                return this.PartialView("_EditProfilePartial", viewModel);
            }

            StripeConfiguration.ApiKey = this.configuration["Stripe:SecretKey"];

            total = Math.Round(total, 2) * 100;
            decimal tempAmount = total / 100;

            var chargeOptions = new ChargeCreateOptions()
            {
                Amount = Convert.ToInt64(total),
                Currency = "CAD",
                Description = "Selling organic food",
                Source = stripeToken,
                ReceiptEmail = stripeEmail
            };

            var chargeService = new ChargeService();
            Charge charge = chargeService.Create(chargeOptions);

            if (charge.Status == "succeeded")
            {
                service.MakeAnOrderWithStripe(cartId, tempAmount);
                TempData["SuccessMessage"] = MessagesConstants.TheOrderWasSuccessfullyPlaced;
                return this.RedirectToAction("All", "Products");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Products(int id)
        {
            IEnumerable<ShoppingCartProduct> viewModels = service.GetOrderProducts(id);

            return View(viewModels);
        }
    }
}