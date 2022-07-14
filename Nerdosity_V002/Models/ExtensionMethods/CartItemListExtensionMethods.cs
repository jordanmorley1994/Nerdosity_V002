using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nerdosity_V002.Models
{
    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO
            {
                ProductId = ci.Product.ProductId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
