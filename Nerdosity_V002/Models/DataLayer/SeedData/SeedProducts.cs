using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nerdosity_V002.Models
{
    internal class SeedProducts : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasData(
                new Product { ProductId = 1, Name = "Betrayal at House on the Hill", CategoryId = "bGames", Price = 34.99 },
                new Product { ProductId = 2, Name = "Dungeons and Dragons Starter Set", CategoryId = "bGames", Price = 78.99 },
                new Product { ProductId = 3, Name = "FullMetal Alchemist Lanyard", CategoryId = "access", Price = 4.99 },
                new Product { ProductId = 4, Name = "League of Legends Lanyard ", CategoryId = "access", Price = 4.99 },
                new Product { ProductId = 5, Name = "Pokemon Surprise Pack ", CategoryId = "tcGames", Price = 55.99 },
                new Product { ProductId = 6, Name = "Diablo III", CategoryId = "vGames", Price = 32.99 },
                new Product { ProductId = 7, Name = "Soul Eater Hoodie", CategoryId = "apparel", Price = 42.99 }
            );
        }
    }
}
