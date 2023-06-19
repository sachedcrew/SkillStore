using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using SklStore.Data;
using SklStore.Models;
using Stripe;

namespace SklStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<StoreDbContext>(options => { options.UseSqlServer(connectionString); });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
            builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
            builder.Services.AddRazorPages();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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

            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("catpage",
                    "{category}/Page{productPage:int}",
                    new { Controller = "Home", action = "Index" });
            app.MapControllerRoute("page", "Page{productPage:int}",
                new { Controller = "Home", action = "Index", productPage = 1 });
            app.MapControllerRoute("category", "{category}",
                new { Controller = "Home", action = "Index", productPage = 1 });
            app.MapControllerRoute("pagination",
                "Products/Page{productPage}",
                new { Controller = "Home", action = "Index", productPage = 1 });
          
            app.MapRazorPages();
            app.MapBlazorHub();
            app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
            SeedData.EnsurePopulated(app);
            StripeConfiguration.SetApiKey(builder.Configuration["Stripe:TestSecretKey"]);

            app.Run();
        }
    }
}