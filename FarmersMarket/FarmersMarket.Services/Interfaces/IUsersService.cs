﻿namespace FarmersMarket.Services.Interfaces
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;

    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetAllUsers();
        Task AddUserToRoleManager(string id);
        Task RemoveUserFromRoleManager(string id);
        void AssignManagerToFarm(string UserId, int FarmId);
        string GetCurrentUserId();
        Task<User> GetCurrentUser();
    }
}
