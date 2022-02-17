using FarmersMarket.Data;
using FarmersMarket.Models.EntityModels;
using FarmersMarket.Services.Implementations;
using FarmersMarket.Services.Interfaces;
using FarmersMarket.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmersMarket.Web.Hubs;
using FarmersMarket.Web.Resources;
using System.Reflection;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FarmersMarketDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<FarmersMarketDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSignalR();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddAuthentication().AddFacebook(fb =>
{
    fb.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    fb.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
});

builder.Services.AddMvc(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization(option =>
{
    var type = typeof(Resource);
    var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
    var factory = builder.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
    var localizer = factory.Create("Resource", assemblyName.Name);
    option.DataAnnotationLocalizerProvider = (t, f) => localizer;
});

builder.Services.AddTransient<IFarmsService, FarmsService>();
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IProductsService, ProductsService>();
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IProfileService, ProfileService>();

var app = builder.Build();

app.UseDatabaseMigration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supportedCultures = new[] { "en-US", "bg" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationsHub>("/notificationshub");
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapDefaultControllerRoute();
});

app.MapRazorPages();

app.Run();
