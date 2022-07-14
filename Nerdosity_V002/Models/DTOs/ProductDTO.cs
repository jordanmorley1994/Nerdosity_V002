using System.Collections.Generic;

namespace Nerdosity_V002.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Dictionary<int, string> Manufacturers { get; set; }

        public void Load(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Price = product.Price;
            Manufacturers = new Dictionary<int, string>();
            foreach (ProductManufacturer pm in product.ProductManufacturers)
            {
                Manufacturers.Add(pm.Manufacturer.ManufacturerId, pm.Manufacturer.Name);
            }
        }
    }
}
