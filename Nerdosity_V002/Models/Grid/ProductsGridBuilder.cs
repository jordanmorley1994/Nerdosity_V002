using Microsoft.AspNetCore.Http;

namespace Nerdosity_V002.Models
{
    public class ProductsGridBuilder : GridBuilder
    {
        public ProductsGridBuilder(ISession sess) : base(sess) { }
        public ProductsGridBuilder(ISession sess, ProductsGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Category.IndexOf(FilterPrefix.Category) == -1;
            routes.ManufacturerFilter = (isInitial) ? FilterPrefix.Manufacturer + values.Manufacturer : values.Manufacturer;
            routes.CategoryFilter = (isInitial) ? FilterPrefix.Category + values.Category : values.Category;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
        }

        public void LoadFilterSegments(string[] filter, Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                routes.ManufacturerFilter = FilterPrefix.Manufacturer + filter[0];
            }
            else
            {
                routes.ManufacturerFilter = FilterPrefix.Manufacturer + filter[0]
                    + "-" + manufacturer.Name.Slug();
            }
            routes.CategoryFilter = FilterPrefix.Category + filter[1];
            routes.PriceFilter = FilterPrefix.Price + filter[2];
        }

        public void ClearFilterSegments() => routes.ClearFilters();

        string def = ProductsGridDTO.DefaultFilter;
        public bool IsFilterByManufacturer => routes.ManufacturerFilter != def;
        public bool IsFilterByCategory => routes.CategoryFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        public bool IsSortByCategory =>
            routes.SortField.EqualsNoCase(nameof(Category));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Product.Price));
    }
}
