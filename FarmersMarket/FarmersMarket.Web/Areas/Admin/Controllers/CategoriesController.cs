namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Models.ViewModels;
    using Models.BindingModels;
    using FarmersMarket.Services.Interfaces;
    using FarmersMarket.Web.Infrastructure;

    public class CategoriesController : BaseAdminController
    {
        private readonly ICategoriesService service;

        public CategoriesController(ICategoriesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories = service.GetAllCategories();

            return View(categories);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCategories()
        {
            IEnumerable<CategoryViewModel> categories = service.GetActiveCategories();

            return Json(new { data = categories });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.service.CreateNewCategory(model);

                this.TempData["SuccessMessage"] = MessagesConstants.CreateCategorySuccessMessage;

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            CategoryViewModel viewModel = service.GetEditCategory(id);

            if (viewModel == null)
            {
                return new NotFoundResult();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditCategory(model);

                return RedirectToAction("Index");
            }

            return this.View();

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            CategoryViewModel viewModel = service.GetDeleteCategory(id);

            if (viewModel == null)
            {
                return new NotFoundResult();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            service.DeleteCategory(id);

            return RedirectToAction("Index");
        }
    }
}
