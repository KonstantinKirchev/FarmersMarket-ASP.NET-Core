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

        public ShoppingCartController(IShoppingCartService service, IProfileService userService)
        {
            this.service = service;
            this.userService = userService;
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

        [HttpGet]
        [Route("/shoppingcart/placeorderwithstripe/{id}")]
        public ActionResult PlaceOrderWithStripe(int id, decimal totalAmount)
        {
            if (!service.IsProfileComplete(this.userService.GetCurrentUser().Result))
            {
                UserViewModel viewModel = userService.GetProfile();
                return this.PartialView("_EditProfilePartial", viewModel);
            }

            ShoppingCart shoppingCart = service.GetShoppingCart(this.userService.GetCurrentUser().Result);

            StripeConfiguration.ApiKey = "sk_test_51IkXZJCdKFy9M6tTaxjIzVYuu97QpMITGvFx1oSAWEZmr9JIWojPyUrBosYCJdcFyW42mb8ZJoLA84ZJZ1KxHGXg00cV0BRyKr";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions> { },
                SuccessUrl = "https://localhost:7177/Home",
                CancelUrl = "https://localhost:7177/ShoppingCart"
            };

            foreach (var package in shoppingCart.ShoppingCartProducts)
            {
                options.LineItems.Add(
                    new SessionLineItemOptions
                    {
                        Name = package.Product.Name,
                        Description = package.Product.Description,
                        Amount = (long?)(package.Product.Price * 100),
                        Currency = "cad",
                        Images = new List<string> { package.Product.ImageUrl },
                        Quantity = 1
                    }
                );
            }

            var sessionService = new SessionService();
            Session session = sessionService.Create(options);

            service.MakeAnOrderWithStripe(id, totalAmount);

            return View(session);
        }

        [HttpGet]
        public IActionResult Products(int id)
        {
            IEnumerable<ShoppingCartProduct> viewModels = service.GetOrderProducts(id);

            return View(viewModels);
        }
    }
}