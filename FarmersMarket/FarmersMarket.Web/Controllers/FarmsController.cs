namespace FarmersMarket.Web.Controllers
{
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using X.PagedList;

    [Route("farms")]
    public class FarmsController : BaseController
    {
        private readonly IFarmsService service;

        public FarmsController(IFarmsService service)
        {
            this.service = service;
        }

        [HttpGet]
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
        public IActionResult All(int? page)
        {
            var farms = this.service.GetAllFarms();

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(farms.ToPagedList(pageNumber, pageSize));
        }
    }
}