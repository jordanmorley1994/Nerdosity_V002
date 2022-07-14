using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nerdosity_V002.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        [StringLength(200)]
        public string Name { get; set; }

        [Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<ProductManufacturer> ProductManufacturers { get; set; }
    }
}
