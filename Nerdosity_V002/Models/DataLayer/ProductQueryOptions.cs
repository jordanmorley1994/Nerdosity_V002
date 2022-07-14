using System.Linq;

namespace Nerdosity_V002.Models
{
    public class ProductQueryOptions : QueryOptions<Product>
    {
        public void SortFilter(ProductsGridBuilder builder)
        {
            if (builder.IsFilterByCategory)
            {
                Where = p => p.CategoryId == builder.CurrentRoute.CategoryFilter;
            }
            if (builder.IsFilterByPrice)
            {
                if (builder.CurrentRoute.PriceFilter == "under20")
                    Where = p => p.Price < 20;
                else if (builder.CurrentRoute.PriceFilter == "20to40")
                    Where = p => p.Price >= 20 && p.Price <= 40;
                else
                    Where = p => p.Price > 50;
            }
            if (builder.IsFilterByManufacturer)
            {
                int id = builder.CurrentRoute.ManufacturerFilter.ToInt();
                if (id > 0)
                    Where = p => p.ProductManufacturers.Any(pm => pm.Manufacturer.ManufacturerId == id);
            }
            if (builder.IsSortByCategory)
            {
                OrderBy = p => p.Category.Name;
            }
            else if (builder.IsSortByPrice)
            {
                OrderBy = p => p.Price;
            }
            else
            {
                OrderBy = p => p.Name;
            }
        }
    }
}
