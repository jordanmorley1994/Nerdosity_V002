using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nerdosity_V002.Models
{
    internal class SeedCategories : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasData(
                new Category { CategoryId = "bGames", Name = "Board Games" },
                new Category { CategoryId = "tcGames", Name = " Trading Card Games" },
                new Category { CategoryId = "vGames", Name = "Video Games" },
                new Category { CategoryId = "access", Name = "Accessories" },
                new Category { CategoryId = "collect", Name = "Collectables" },
                new Category { CategoryId = "cbooks", Name = "Comic Books" },
                new Category { CategoryId = "apparel", Name = "Apparel" }
                );
        }
    }
}
