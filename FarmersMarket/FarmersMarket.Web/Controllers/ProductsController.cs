namespace FarmersMarket.Web.Controllers
{
    using System.Collections.Generic;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using X.PagedList;

    [Route("products")]
    public class ProductsController : BaseController
    {
        private readonly IProductsService service;

        public ProductsController(IProductsService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("{category?}")]
        public IActionResult All(string category, int? page)
        {
            IEnumerable<ProductViewModel>  products = category == null ? this.service.GetAllProducts() : this.service.GetFilteredProducts(category);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(string currentFilter, string product, int? page)
        {
            if (product != null)
            {
                page = 1;
            }
            else
            {
                product = currentFilter;

            }

            ViewBag.CurrentFilter = product;

            var products = this.service.GetSearchedProducts(product);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return this.View(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("farm/{farmName}")]
        public IActionResult ByFarm(string farmName, int? page)
        {
            var products = this.service.GetProductsByFarm(farmName);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }
    }
}