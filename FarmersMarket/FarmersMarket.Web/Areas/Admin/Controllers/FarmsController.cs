namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class FarmsController : BaseAdminController
    {
        private readonly IFarmsService service;

        public FarmsController(IFarmsService service) 
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(FarmBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.CreateNewFarm(model);

                return RedirectToAction("All", "Farms", routeValues: new { area = "" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("{id}/edit")]
        public IActionResult Edit(int id)
        {
            FarmViewModel? viewModel = service.GetEditFarm(id);

            if (viewModel == null)
            {
                return new NotFoundResult();
            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("{id}/edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FarmBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditFarm(model);

                return RedirectToAction("All", "Farms", routeValues: new { area = "" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("{id}/delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            FarmViewModel? viewModel = service.GetDeleteFarm(id);

            if (viewModel == null)
            {
                return new NotFoundResult();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("{id}/delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            service.DeleteFarm(id);
           
            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }
    }
}