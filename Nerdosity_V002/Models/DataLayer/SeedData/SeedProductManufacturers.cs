using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nerdosity_V002.Models
{
    internal class SeedProductManufacturers : IEntityTypeConfiguration<ProductManufacturer>
    {
        public void Configure(EntityTypeBuilder<ProductManufacturer> entity)
        {
            entity.HasData(
                new ProductManufacturer { ProductId = 1, ManufacturerId = 1 },
                new ProductManufacturer { ProductId = 2, ManufacturerId = 4 },
                new ProductManufacturer { ProductId = 3, ManufacturerId = 3 },
                new ProductManufacturer { ProductId = 4, ManufacturerId = 3 },
                new ProductManufacturer { ProductId = 5, ManufacturerId = 5 },
                new ProductManufacturer { ProductId = 6, ManufacturerId = 6 },
                new ProductManufacturer { ProductId = 7, ManufacturerId = 7 }
                );
        }
    }
}
