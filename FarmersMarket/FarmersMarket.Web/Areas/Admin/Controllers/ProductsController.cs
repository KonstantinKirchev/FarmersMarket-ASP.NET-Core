namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using FarmersMarket.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseAdminController
    {
        private readonly IProductsService service;

        public ProductsController(IProductsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> products = service.GetAllProducts();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel model = service.GetAddProduct();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.CreateNewProduct(model);

                this.TempData["SuccessMessage"] = MessagesConstants.CreateNewProductSuccessMessage;

                return RedirectToAction("Index", "Products", routeValues: new { area = "Admin" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("Admin/Products/{id}/Edit")]
        public IActionResult Edit(int id)
        { 
            ProductViewModel? viewModel = service.GetEditProduct(id);

            return View(viewModel);
        }

        [HttpPost]
        [Route("Admin/Products/{id}/Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditProduct(model);

                return RedirectToAction("Index", "Products", routeValues: new { area = "Admin" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("Admin/Products/{id}/Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ProductViewModel? viewModel = service.GetDeleteProduct(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Admin/Products/{id}/Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            service.DeleteProduct(id);

            return RedirectToAction("Index", "Products", routeValues: new { area = "Admin" });
        }
    }
}