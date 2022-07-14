using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Nerdosity_V002.Models
{
    public class NerdosityContext : IdentityDbContext<User>
    {
        public NerdosityContext(DbContextOptions<NerdosityContext> options)
            : base(options)
        { }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductManufacturer> ProductManufacturers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductManufacturer>().HasKey(pm => new { pm.ProductId, pm.ManufacturerId });

            modelBuilder.Entity<ProductManufacturer>().HasOne(pm => pm.Product)
                .WithMany(p => p.ProductManufacturers)
                .HasForeignKey(pm => pm.ProductId);

            modelBuilder.Entity<ProductManufacturer>().HasOne(pm => pm.Manufacturer)
                .WithMany(m => m.ProductManufacturers)
                .HasForeignKey(pm => pm.ManufacturerId);

            modelBuilder.Entity<Product>().HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new SeedCategories());
            modelBuilder.ApplyConfiguration(new SeedProducts());
            modelBuilder.ApplyConfiguration(new SeedManufacturers());
            modelBuilder.ApplyConfiguration(new SeedProductManufacturers());

        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Password";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

    }
}
