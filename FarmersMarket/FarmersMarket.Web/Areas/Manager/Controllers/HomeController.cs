namespace FarmersMarket.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseManagerController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
