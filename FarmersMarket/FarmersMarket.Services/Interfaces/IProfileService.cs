namespace FarmersMarket.Services.Interfaces
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;

    public interface IProfileService
    {
        UserViewModel GetProfile();
        IEnumerable<MyOrderViewModel> GetMyOrders();
        IEnumerable<MyOrderViewModel> GetOrdersByStatus(string status);
        void EditUser(UserBindingModel model);
        Task<User> GetCurrentUser();
    }
}
