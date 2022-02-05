namespace FarmersMarket.Data
{
    using FarmersMarket.Models.EntityModels;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FarmersMarketDbContext : IdentityDbContext
    {
        public FarmersMarketDbContext(DbContextOptions<FarmersMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Farm> Farms { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShoppingCartProduct>().HasKey(q =>
                new
                {
                    q.ShoppingCartId,
                    q.ProductId
                });

            builder.Entity<ShoppingCartProduct>()
                .HasOne(t => t.ShoppingCart)
                .WithMany(t => t.ShoppingCartProducts)
                .HasForeignKey(t => t.ShoppingCartId);

            builder.Entity<ShoppingCartProduct>()
                .HasOne(t => t.Product)
                .WithMany(t => t.ShoppingCartProducts)
                .HasForeignKey(t => t.ProductId);

            base.OnModelCreating(builder);
        }
    }
}