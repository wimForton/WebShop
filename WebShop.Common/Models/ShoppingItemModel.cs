using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ShoppingItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int myProductid { get; set; }
        public int ProductCategory { get; set; }
        //public ProductModel myProduct { get; set; }
        public MoonModel myProduct { get; set; }
        public int myShoppingBagId { get; set; }
        public ShoppingBagModel myShoppingBag { get; set; }

    }
}
