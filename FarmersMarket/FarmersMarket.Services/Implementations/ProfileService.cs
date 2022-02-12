namespace FarmersMarket.Services.Implementations
{
    using AutoMapper;
    using FarmersMarket.Data;
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.Enums;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class ProfileService : Service, IProfileService
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProfileService(FarmersMarketDbContext db, IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
            : base(db, mapper)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public UserViewModel GetProfile()
        {
            UserViewModel viewModel = this.mapper.Map<User, UserViewModel>(this.GetCurrentUser().Result);

            return viewModel;
        }

        public IEnumerable<MyOrderViewModel> GetMyOrders()
        {
            var user = this.GetCurrentUser().Result;

            IEnumerable<ShoppingCart> shoppingcarts =
                this.db.ShoppingCarts
                    .Where(s => s.UserId == user.Id)
                    .OrderByDescending(s => s.DateOfOrder)
                    .ToList();

            IEnumerable<MyOrderViewModel> viewModels = this.mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<MyOrderViewModel>>(shoppingcarts);

            return viewModels;
        }

        public IEnumerable<MyOrderViewModel> GetOrdersByStatus(string status)
        {
            var user = this.GetCurrentUser().Result;
            IEnumerable<ShoppingCart> orders;

            if (status != "All")
            {
                OrderStatus currentStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), status);
                orders = this.db.ShoppingCarts.Where(s => s.UserId == user.Id && s.Status == currentStatus).ToList();
            }
            else
            {
                orders = this.db.ShoppingCarts.Where(s => s.UserId == user.Id).ToList();
            }

            IEnumerable<MyOrderViewModel> viewModels = this.mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<MyOrderViewModel>>(orders);

            return viewModels;
        }

        public void EditUser(UserBindingModel model)
        {
            var user = this.GetCurrentUser().Result;

            user.Name = model.Name;
            user.Email = model.Email;
            user.ImageUrl = model.ImageUrl;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            this.db.SaveChanges();
        }

        public async Task<User> GetCurrentUser() => await this.userManager.GetUserAsync(this.httpContextAccessor.HttpContext.User);
    }
}
