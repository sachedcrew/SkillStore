using Microsoft.EntityFrameworkCore;

namespace SklStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Python Developer",
                        Description = "Python przeznaczona dla jednej osoby",
                        Category = "Programowanie",
                        Price = 275,
                        Popularity = 0,
                        IsSale = false,
                        IsActive = false

                    },
                    new Product
                    {
                        Name = "Java Developer",
                        Description = "Java przeznaczona dla jednej osoby",
                        Category = "Marketing",
                        Price = 375,
                        Popularity = 0,
                        IsSale = false,
                        IsActive = false
                    },
                    new Product
                    {
                        Name = "Pytho234n Developer",
                        Description = "Python przeznaczona dla jednej osoby",
                        Category = "Programowanie",
                        Price = 275,
                        Popularity = 0,
                        IsSale = false,
                        IsActive = false

                    },
                    new Product
                    {
                        Name = "Java234 Developer",
                        Description = "Java przeznaczona dla jednej osoby",
                        Category = "Marketing",
                        Price = 375,
                        Popularity = 0,
                        IsSale = false,
                        IsActive = false
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
