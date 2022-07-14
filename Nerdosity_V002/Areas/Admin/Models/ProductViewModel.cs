using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Nerdosity_V002.Models
{
    public class ProductViewModel : IValidatableObject
    {
        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public int[] SelectedManufacturers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (SelectedManufacturers == null)
            {
                yield return new ValidationResult(
                  "Please select at least one manufacturer.",
                  new[] { nameof(SelectedManufacturers) });
            }
        }
    }
}
