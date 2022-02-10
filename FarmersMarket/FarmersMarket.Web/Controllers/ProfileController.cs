namespace FarmersMarket.Web.Controllers
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using X.PagedList;

    public class ProfileController : BaseController
    {
        private readonly IProfileService service;
        private readonly UserManager<User> userManager;
    
        public ProfileController(IProfileService service, UserManager<User> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            UserViewModel viewModel = service.GetProfile();

            TempData["controllerName"] = this.ControllerContext.RouteData.Values["controller"]?.ToString();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            UserViewModel viewModel = service.GetProfile();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditUser(model);

                string controller = TempData["controllerName"].ToString();

                return RedirectToAction("Index", controller, routeValues: new { area = "" });

            }

            return this.View();
        }

        [HttpGet]
        public IActionResult MyOrders(int? page)
        {
            IEnumerable<MyOrderViewModel> viewModels = service.GetMyOrders();

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(viewModels.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult OrdersByStatusPartial(string status, int? page)
        {            
            IEnumerable<MyOrderViewModel> viewModels = service.GetOrdersByStatus(status);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return PartialView("_OrdersByStatusPartial", viewModels.ToPagedList(pageNumber, pageSize));
        }
    }
}