namespace FarmersMarket.Web.Areas.Manager.Controllers
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using FarmersMarket.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseManagerController
    {
        private readonly IProductsService productsService;
        private readonly IUsersService usersService;

        public ProductsController(IProductsService productsService, IUsersService usersService)
        {
            this.productsService = productsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await this.usersService.GetCurrentUser();
            int farmId = (int)user.FarmId;
            IEnumerable<ProductViewModel> products = productsService.GetAllFarmProducts(farmId);
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel model = productsService.GetAddProduct();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                productsService.CreateNewProduct(model);

                this.TempData["SuccessMessage"] = MessagesConstants.CreateNewProductSuccessMessage;

                return RedirectToAction("Index", "Products", routeValues: new { area = "Manager" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("Manager/Products/{id}/Edit")]
        public IActionResult Edit(int id)
        {
            ProductViewModel viewModel = productsService.GetEditProduct(id);

            return View(viewModel);
        }

        [HttpPost]
        [Route("Manager/Products/{id}/Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                productsService.EditProduct(model);

                return RedirectToAction("Index", "Products", routeValues: new { area = "Manager" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("Manager/Products/{id}/Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ProductViewModel viewModel = productsService.GetDeleteProduct(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Manager/Products/{id}/Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            productsService.DeleteProduct(id);

            return RedirectToAction("Index", "Products", routeValues: new { area = "Manager" });
        }
    }
}
