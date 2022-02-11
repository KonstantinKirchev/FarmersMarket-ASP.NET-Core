namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using FarmersMarket.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class FarmsController : BaseAdminController
    {
        private readonly IFarmsService service;

        public FarmsController(IFarmsService service) 
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<FarmViewModel> farms = service.GetAllFarms();

            return View(farms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FarmBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.CreateNewFarm(model);

                this.TempData["SuccessMessage"] = MessagesConstants.CreateNewFarmSuccessMessage;

                return RedirectToAction("Index", "Farms", routeValues: new { area = "Admin" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("Admin/Farms/{id}/Edit")]
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
        [Route("Admin/Farms/{id}/Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FarmBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditFarm(model);

                return RedirectToAction("Index", "Farms", routeValues: new { area = "Admin" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("Admin/Farms/{id}/delete")]
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
        [Route("Admin/Farms/{id}/delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            service.DeleteFarm(id);
           
            return RedirectToAction("Index", "Farms", routeValues: new { area = "Admin" });
        }
    }
}