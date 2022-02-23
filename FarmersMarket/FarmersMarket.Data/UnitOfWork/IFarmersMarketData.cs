namespace FarmersMarket.Data.UnitOfWork
{
    using FarmersMarket.Data.Repositories;
    using FarmersMarket.Models.EntityModels;

    public interface IFarmersMarketData
    {
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Farm> Farms { get; }

        IRepository<Product> Products { get; }

        IRepository<ShoppingCart> ShoppingCarts { get; }

        IRepository<ShoppingCartProduct> ShoppingCartProducts { get; }

        int SaveChanges();
    }
}
