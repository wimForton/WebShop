using WebShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DAL.Data.Repositories.ShoppingBag
{
    public interface IShoppingBagRepository
    {
        //public void CreateShoppingBag(string userName);
        public Task CreateShoppingBagAsync(string userName);
        public Task<List<int>> GetShoppingItemsByUserAsync(string username);
        public Task<ShoppingBagModel> GetShoppingBagByIdAsync(int? id);
        public Task<ShoppingBagModel> GetShoppingBagWithDataByIdAsync(int? id);
        public Task<bool> DeleteConfirmedAsync(int? id);
        public Task<string> GetShoppingBagUserByIdAsync(int? id);
        public int AddItem(IdentityUser user, int myProductId, int ProductCategory, int myQuantity);
        public Task RemoveItem(int myItemId);
    }
}
