using Microsoft.AspNetCore.SignalR;

namespace WomenMarket.App.Hubs
{

    //[HubName("notifications")]
    public class NotificationsHub : Hub
    {
        public void SendNotification(string type, string notification)
        {
            //this.Clients.Others.receiveNotification(type, notification);
        }
    }
}