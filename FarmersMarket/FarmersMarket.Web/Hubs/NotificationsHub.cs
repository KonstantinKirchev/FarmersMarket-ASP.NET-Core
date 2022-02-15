namespace FarmersMarket.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class NotificationsHub : Hub, INotificationsHub
    {
        protected IHubContext<Hub> context;

        public NotificationsHub(IHubContext<Hub> context)
        {
            this.context = context;
        }

        public async Task SendNotification(string type, string notification)
        {
            await this.context.Clients.All.SendAsync("Send", type, notification);
        }
    }
}