using System.Collections.Generic;

namespace Nerdosity_V002.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Dictionary<string, string> Prices =>
            new Dictionary<string, string> {
                { "under20", "Under $20" },
                { "20to40", "$20 to $40" },
                { "over40", "Over $40" }
            };
    }
}
