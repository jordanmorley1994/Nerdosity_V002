using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Nerdosity_V002.Models
{
    public class Validate
    {
        private const string CategoryKey = "validCategory";
        private const string ManufacturerKey = "validManufacturer";
        private const string EmailKey = "validEmail";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckCategory(string categoryId, Repository<Category> data)
        {
            Category entity = data.Get(categoryId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Category id {categoryId} is already in the database.";
        }
        public void MarkCategoryChecked() => tempData[CategoryKey] = true;
        public void ClearCategory() => tempData.Remove(CategoryKey);
        public bool IsCategoryChecked => tempData.Keys.Contains(CategoryKey);

        public void CheckManufacturer(string name, string address, string operation, Repository<Manufacturer> data)
        {
            Manufacturer entity = null;
            if (Operation.IsAdd(operation))
            {
                entity = data.Get(new QueryOptions<Manufacturer>
                {
                    Where = m => m.Name == name && m.Address == address
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Manufacturer {entity.Name} is already in the database.";
        }
        public void MarkManufacturerChecked() => tempData[ManufacturerKey] = true;
        public void ClearManufacturer() => tempData.Remove(ManufacturerKey);
        public bool IsManufacturerChecked => tempData.Keys.Contains(ManufacturerKey);
    }
}
