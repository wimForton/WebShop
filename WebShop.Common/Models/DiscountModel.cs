using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class DiscountModel
    {
        public int Id { get; set; }
        public int MinimumPurchase { get; set; }
        public double DiscountPercentage { get; set; }
    }
}
