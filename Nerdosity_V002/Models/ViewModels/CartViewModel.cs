using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nerdosity_V002.Models
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> List { get; set; }
        public double Subtotal { get; set; }
        public RouteDictionary ProductGridRoute { get; set; }
    }
}
