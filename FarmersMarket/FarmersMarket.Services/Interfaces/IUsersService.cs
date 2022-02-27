namespace FarmersMarket.Services.Interfaces
{
    using FarmersMarket.Models.ViewModels;

    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetAllUsers();
        Task AddUserToRoleManager(string id);

        Task RemoveUserFromRoleManager(string id);
    }
}
