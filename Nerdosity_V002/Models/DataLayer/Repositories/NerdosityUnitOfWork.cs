using System.Linq;

namespace Nerdosity_V002.Models
{
    public class NerdosityUnitOfWork : INerdosityUnitOfWork
    {
        private NerdosityContext context { get; set; }
        public NerdosityUnitOfWork(NerdosityContext ctx) => context = ctx;

        private Repository<Product> productData;
        public Repository<Product> Products
        {
            get
            {
                if (productData == null)
                    productData = new Repository<Product>(context);
                return productData;
            }
        }

        private Repository<Manufacturer> manufacturerData;
        public Repository<Manufacturer> Manufacturers
        {
            get
            {
                if (manufacturerData == null)
                    manufacturerData = new Repository<Manufacturer>(context);
                return manufacturerData;
            }
        }

        private Repository<ProductManufacturer> productmanufacturerData;
        public Repository<ProductManufacturer> ProductManufacturers
        {
            get
            {
                if (productmanufacturerData == null)
                    productmanufacturerData = new Repository<ProductManufacturer>(context);
                return productmanufacturerData;
            }
        }

        private Repository<Category> categoryData;
        public Repository<Category> Categories
        {
            get
            {
                if (categoryData == null)
                    categoryData = new Repository<Category>(context);
                return categoryData;
            }
        }

        public void DeleteCurrentProductManufacturers(Product product)
        {
            var currentManufacturers = ProductManufacturers.List(new QueryOptions<ProductManufacturer>
            {
                Where = pm => pm.ProductId == product.ProductId
            });
            foreach (ProductManufacturer pm in currentManufacturers)
            {
                ProductManufacturers.Delete(pm);
            }
        }

        public void LoadNewProductManufacturers(Product product, int[] manufacturerids)
        {
            product.ProductManufacturers = manufacturerids.Select(i =>
                new ProductManufacturer { Product = product, ManufacturerId = i })
                .ToList();
        }

        public void Save() => context.SaveChanges();
    }
}
