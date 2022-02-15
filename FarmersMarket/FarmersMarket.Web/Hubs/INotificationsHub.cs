namespace FarmersMarket.Web.Hubs
{
    public interface INotificationsHub
    {
        Task SendNotification(string type, string notification);
    }
}
