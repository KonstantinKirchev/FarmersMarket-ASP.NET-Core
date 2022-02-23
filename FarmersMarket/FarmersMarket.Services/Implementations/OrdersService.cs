namespace FarmersMarket.Services.Implementations
{
    using AutoMapper;
    using FarmersMarket.Data.UnitOfWork;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.Enums;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;

    public class OrdersService : Service, IOrdersService
    {
        public OrdersService(IFarmersMarketData db, IMapper mapper) 
            : base(db, mapper)
        {
        }

        public IEnumerable<OrderViewModel> GetAllOrders()
        {
            IEnumerable<ShoppingCart> orders = this.db.ShoppingCarts.All().OrderByDescending(o => o.DateOfOrder).ToList();
            IEnumerable<OrderViewModel> viewModels = this.mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<OrderViewModel>>(orders);

            return viewModels;
        }

        public void DeliverOrder(int id)
        {
            ShoppingCart? order = this.db.ShoppingCarts.Find(id);

            if (order != null)
            {
                order.Status = OrderStatus.Delivered;
                order.DateOfDelivery = DateTime.Now;
                this.db.SaveChanges();
            }
        }

        public IEnumerable<OrderViewModel> GetOrdersByStatus(string status)
        {
            IEnumerable<ShoppingCart> orders;

            if (status == "All")
            {
                orders = this.db.ShoppingCarts.All().ToList();

            }
            else
            {
                OrderStatus currentStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), status);
                orders = this.db.ShoppingCarts.All().Where(s => s.Status == currentStatus).ToList();
            }

            IEnumerable<OrderViewModel> viewModels = this.mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<OrderViewModel>>(orders);

            return viewModels;
        }

        public IEnumerable<ShoppingCartProduct> GetOrderProducts(int id)
        {
            var products =
                this.db.ShoppingCartProducts
                .All()
                .Where(sp => sp.ShoppingCartId == id).ToList();

            return products;
        }

        public UserViewModel? GetOrderOwner(int id)
        {
            var userId = this.db.ShoppingCarts.Find(id)?.UserId;
            User? user = this.db.Users.Find(userId);

            if (user != null)
            {
                UserViewModel viewModel = this.mapper.Map<User, UserViewModel>(user);

                return viewModel;
            }

            return null;
        }
    }
}
