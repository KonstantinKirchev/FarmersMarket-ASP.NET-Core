namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using X.PagedList;

    public class OrdersController : BaseAdminController
    {
        private readonly IOrdersService service;

        public OrdersController(IOrdersService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            IEnumerable<OrderViewModel> viewModels = service.GetAllOrders();

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(viewModels.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult DeliverOrder(int id)
        {
            service.DeliverOrder(id);

            return this.RedirectToAction("Index","Orders");
        }

        public IActionResult OrdersByStatusPartial(string status, int? page)
        {
            IEnumerable<OrderViewModel> viewModels = service.GetOrdersByStatus(status);
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return PartialView("_OrdersByStatusPartial", viewModels.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("{id}/products/details")]
        public IActionResult Products(int id)
        {
            IEnumerable<ShoppingCartProduct> viewModels = service.GetOrderProducts(id);
            
            return View(viewModels);
        }

        [HttpGet]
        [Route("{id}/client")]
        public IActionResult Client(int id)
        {
            UserViewModel user = service.GetOrderOwner(id);

            return View(user);
        }
    }
}