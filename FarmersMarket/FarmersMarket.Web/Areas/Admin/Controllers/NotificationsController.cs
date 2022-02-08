namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class NotificationsController : BaseAdminController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult? SendNotification(string type, string notification)
        {
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

            //hubContext.Clients.All.receiveNotification(type, notification);

            return null;
        } 
    }
}