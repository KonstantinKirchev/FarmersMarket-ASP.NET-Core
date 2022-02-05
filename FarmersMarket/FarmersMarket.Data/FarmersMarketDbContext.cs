namespace FarmersMarket.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FarmersMarketDbContext : IdentityDbContext
    {
        public FarmersMarketDbContext(DbContextOptions<FarmersMarketDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}