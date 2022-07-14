using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nerdosity_V002.Models
{
    public class CartItem
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double Subtotal => Product.Price * Quantity;
    }
}
