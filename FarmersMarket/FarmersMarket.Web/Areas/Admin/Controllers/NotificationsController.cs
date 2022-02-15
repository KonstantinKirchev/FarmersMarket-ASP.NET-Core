namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using FarmersMarket.Web.Hubs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    public class NotificationsController : BaseAdminController
    {
        protected IHubContext<NotificationsHub> context;

        public NotificationsController(IHubContext<NotificationsHub> context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task Notification(string type, string notification)
        {
            var notificationHub = new NotificationsHub(this.context);
            await notificationHub.SendNotification(type, notification);
        } 
    }
}