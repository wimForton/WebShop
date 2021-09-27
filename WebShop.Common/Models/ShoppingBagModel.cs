using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ShoppingBagModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ShoppingItemModel> myShoppingItems { get; set; }
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
