﻿namespace FarmersMarket.Web.Controllers
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("shoppingcart")]
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
                return this.RedirectToAction("Edit", "Profile");
            }

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return View(carts);
        }

        [HttpGet]
        [Route("addproduct/{id}")]
        public IActionResult AddProduct(int id)
        {
            Product? product = service.GetProduct(id);

            ShoppingCart cart = service.GetShoppingCart(this.userService.GetCurrentUser().Result);

            if (product != null)
            {
                service.AddToShoppingCart(cart, product);
            }

            return this.RedirectToAction("All", "Products");
        }

        [HttpGet]
        [Route("cart/{cartId}/removeproduct/{id}")]
        public IActionResult RemoveProduct(int cartId, int id)
        {
            Product? product = service.GetProduct(id);

            ShoppingCart? cart = service.GetCurrentShoppingCart(cartId);

            if (product != null && cart != null)
            {
                service.RemoveFromShoppingCart(cart, product);
            }

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("cart/{cartId}/decreaseproductunits/{id}")]
        public IActionResult DecreaseProductUnits(int cartId, int id)
        {
            Product? product = service.GetProduct(id);

            ShoppingCart? cart = service.GetCurrentShoppingCart(cartId);

            if (product != null && cart != null)
            {
                service.DecreaseProductUnitsFromShoppingCart(cart, product);
            }

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("cart/{cartId}/increaseproductunits/{id}")]
        public IActionResult IncreaseProductUnits(int cartId, int id)
        {
            Product? product = service.GetProduct(id);

            ShoppingCart? cart = service.GetCurrentShoppingCart(cartId);

            if (product != null && cart != null)
            {
                service.IncreaseProductUnitsFromShoppingCart(cart, product);
            }

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(this.userService.GetCurrentUser().Result);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("placeorder/{id}")]
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
        public IActionResult Products(int id)
        {
            IEnumerable<ShoppingCartProduct> viewModels = service.GetOrderProducts(id);

            return View(viewModels);
        }
    }
}