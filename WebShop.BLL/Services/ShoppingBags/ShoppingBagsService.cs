using WebShop.DAL.Data.Repositories.ShoppingBag;
using WebShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.BLL.Services.ShoppingBags
{
    public class ShoppingBagsService : IShoppingBagsService
    {
        private readonly IShoppingBagRepository _ShoppingBagsRepository;
        UserManager<IdentityUser> _userManager;
        public ShoppingBagsService(IShoppingBagRepository ShoppingBagsRepository, UserManager<IdentityUser> userManager)
        {
            _ShoppingBagsRepository = ShoppingBagsRepository;
            _userManager = userManager;
        }


        public async Task CreateShoppingBagAsync(string userName)
        {
            await _ShoppingBagsRepository.CreateShoppingBagAsync(userName);
        }
        public int AddItem(IdentityUser user, int myProductId, int ProductCategory, int myQuantity)
        {
            //myQuantity = 1;
            //string test = User.Identity.Name;
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return _ShoppingBagsRepository.AddItem(user, myProductId, ProductCategory, myQuantity);
        }
        public async Task<List<int>> GetShoppingItemsByUserAsync(string username)
        {
            return await _ShoppingBagsRepository.GetShoppingItemsByUserAsync(username);
        }
        public async Task<bool> DeleteConfirmedAsync(int? id)
        {
            return await _ShoppingBagsRepository.DeleteConfirmedAsync(id);
        }

        public async Task<ShoppingBagModel> GetShoppingBagByIdAsync(int? id)
        {
            return await _ShoppingBagsRepository.GetShoppingBagByIdAsync(id);
        }

        public async Task<string> GetShoppingBagUserByIdAsync(int? id)
        {
            return await _ShoppingBagsRepository.GetShoppingBagUserByIdAsync(id);
        }

        public async Task<ShoppingBagModel> GetShoppingBagWithDataByIdAsync(int? id)
        {
            return await _ShoppingBagsRepository.GetShoppingBagWithDataByIdAsync(id);
        }

        public async Task RemoveItem(int myItemId)
        {
            await _ShoppingBagsRepository.RemoveItem(myItemId);
        }
    }
}
