using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public List<DiscountModel> Discounts { get; set; } = new List<DiscountModel>();
        public string Name { get; set; }
        public string Imagepath { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public int ProductCategory { get; set; }
    }
}
