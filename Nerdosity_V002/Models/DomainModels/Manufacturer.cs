using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Nerdosity_V002.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an address.")]
        [MaxLength(200)]
        [Remote("CheckManufacturer", "Validation", "Area",
            AdditionalFields = "Name, Operation")]
        public string Address { get; set; }


        public ICollection<ProductManufacturer> ProductManufacturers { get; set; }
    }
}
