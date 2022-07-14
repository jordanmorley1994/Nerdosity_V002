namespace Nerdosity_V002.Models
{
    public class ProductManufacturer
    {
        public int ProductId { get; set; }
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public Product Product { get; set; }
    }
}
