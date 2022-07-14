using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nerdosity_V002.Models
{
    internal class SeedManufacturers : IEntityTypeConfiguration<Manufacturer>
    {
       public void Configure(EntityTypeBuilder<Manufacturer> entity)
        {
            entity.HasData(
            new Manufacturer { ManufacturerId = 1, Name = "Avalon Hill", Address = "123 Avalon Hill Santa Anita, CA 90210" },
            new Manufacturer { ManufacturerId = 2, Name = "Hasbro", Address = "5226 West Mile Rd Clearwater, TX 75224" },
            new Manufacturer { ManufacturerId = 3, Name = "Lanyard Expressions", Address = "8526 N. 756th St. Urbandale, IA 58112" },
            new Manufacturer { ManufacturerId = 4, Name = "Wizards of the Coast", Address = "9958 Circle K West Orange County, CA 90256" },
            new Manufacturer { ManufacturerId = 5, Name = "The Pokemon Company", Address = "852 hitashi headquarters st 104 Minato, Tokyo, Japan 12355897" },
            new Manufacturer { ManufacturerId = 6, Name = "Blizzard North", Address = "1616 Sierra Parkway San Diego, CA 90244" },
            new Manufacturer { ManufacturerId = 7, Name = "TrendingThreads", Address = "87556 Spruce St. St #2 Lexington, KY 74558" }
         );
        }
    }
}
