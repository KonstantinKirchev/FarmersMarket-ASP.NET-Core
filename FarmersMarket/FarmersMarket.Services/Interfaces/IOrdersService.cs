namespace FarmersMarket.Services.Interfaces
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;

    public interface IOrdersService
    {
        IEnumerable<OrderViewModel> GetAllOrders();
        void DeliverOrder(int id);
        IEnumerable<OrderViewModel> GetOrdersByStatus(string status);
        IEnumerable<ShoppingCartProduct> GetOrderProducts(int id);
        UserViewModel? GetOrderOwner(int id);
    }
}
