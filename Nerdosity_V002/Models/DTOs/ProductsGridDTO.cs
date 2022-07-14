using Newtonsoft.Json;

namespace Nerdosity_V002.Models
{
    public class ProductsGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Manufacturer { get; set; } = DefaultFilter;
        public string Category { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
    }
}
