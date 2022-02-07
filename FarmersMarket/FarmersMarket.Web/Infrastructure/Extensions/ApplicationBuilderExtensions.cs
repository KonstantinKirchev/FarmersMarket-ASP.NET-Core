namespace FarmersMarket.Web.Infrastructure.Extensions
{
    using FarmersMarket.Data;
    using FarmersMarket.Models.EntityModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) 
            {
                serviceScope.ServiceProvider.GetService<FarmersMarketDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    var roles = new[] { WebConstants.AdminRole, WebConstants.ManagerRole };

                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });
                        }
                    }

                    var adminEmail = "admin@gmail.com";

                    var adminExists = await userManager.FindByEmailAsync(adminEmail);

                    if (adminExists == null)
                    {
                        var user = new User
                        {
                            Email = adminEmail,
                            UserName = WebConstants.AdminRole
                        };

                        await userManager.CreateAsync(user, "Admin1234!");
                        await userManager.AddToRoleAsync(user, WebConstants.AdminRole);
                    }

                    var managerEmail = "manager@gmail.com";

                    var managerExists = await userManager.FindByEmailAsync(managerEmail);

                    if (managerExists == null)
                    {
                        var user = new User
                        {
                            Email = managerEmail,
                            UserName = WebConstants.ManagerRole
                        };

                        await userManager.CreateAsync(user, "Manager1234!");
                        await userManager.AddToRoleAsync(user, WebConstants.ManagerRole);
                    }
                })
                .Wait();
            }

            return app;
        }
    }
}
