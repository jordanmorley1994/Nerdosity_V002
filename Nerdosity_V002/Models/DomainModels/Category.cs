using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Nerdosity_V002.Models
{
    public class Category
    {
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter a category id.")]
        [Remote("CheckCategory", "Validation", "Area")]
        public string CategoryId { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Please enter a category name.")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
