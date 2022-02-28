﻿namespace FarmersMarket.Services.Implementations
{
    using AutoMapper;
    using FarmersMarket.Data.UnitOfWork;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : Service, IUsersService
    {
        private readonly UserManager<User> userManager;

        public UsersService(IFarmersMarketData db, IMapper mapper, UserManager<User> userManager) 
            : base(db, mapper)
        {
            this.userManager = userManager;
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            IEnumerable<User> users = this.db.Users
                .All()
                .Where(u => u.Email != "admin@gmail.com" && u.Email != "manager@gmail.com")
                .Include(s => s.Farm)
                .OrderBy(o => o.Name)
                .ToList();
            IEnumerable<UserViewModel> viewModels = this.mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);

            return viewModels;
        }

        public async Task AddUserToRoleManager(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);
            await this.userManager.AddToRoleAsync(user, "Manager");
        }

        public async Task RemoveUserFromRoleManager(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);
            await this.userManager.RemoveFromRoleAsync(user, "Manager");
        }

        public void AssignManagerToFarm(string UserId, int FarmId)
        {
            User? user = this.db.Users.Find(UserId);

            if (user != null)
            {
                user.FarmId = FarmId;
                db.SaveChanges();
            }
        }
    }
}
