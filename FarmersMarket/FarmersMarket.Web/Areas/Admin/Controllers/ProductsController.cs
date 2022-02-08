namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseAdminController
    {
        private readonly IProductsService service;

        public ProductsController(IProductsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ProductViewModel model = service.GetAddProduct();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.CreateNewProduct(model);

                return RedirectToAction("All", "Products", routeValues: new { area = "" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("{id}/edit")]
        public IActionResult Edit(int id)
        { 
            ProductViewModel? viewModel = service.GetEditProduct(id);

            return View(viewModel);
        }

        [HttpPost]
        [Route("{id}/edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditProduct(model);

                return RedirectToAction("All", "Products", routeValues: new { area = "" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("{id}/delete")]
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
        [Route("{id}/delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            service.DeleteProduct(id);

            return RedirectToAction("All", "Products", routeValues: new { area = "" });
        }
    }
}