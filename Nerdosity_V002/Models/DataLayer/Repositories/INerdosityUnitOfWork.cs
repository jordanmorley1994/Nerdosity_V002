namespace Nerdosity_V002.Models
{
    public interface INerdosityUnitOfWork
    {
        Repository<Product> Products { get; }
        Repository<Manufacturer> Manufacturers { get; }
        Repository<ProductManufacturer> ProductManufacturers { get; }
        Repository<Category> Categories { get; }

        void DeleteCurrentProductManufacturers(Product product);
        void LoadNewProductManufacturers(Product product, int[] manufacturerids);
        void Save();
    }
}
