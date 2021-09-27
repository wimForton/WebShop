using WebShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DAL.Data.Repositories.ShoppingBag
{
    public class ShoppingBagRepository : IShoppingBagRepository
    {
        private readonly WebShopContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ShoppingBagRepository(UserManager<IdentityUser> userManager, WebShopContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task CreateShoppingBagAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);///Ophalen user op basis van ingelogde persoon
            //user.Wait();
            ShoppingBagModel MyShoppingBag = new ShoppingBagModel() { IdentityUserId = user.Id, Date = DateTime.Now };
            _context.ShoppingBags.Add(MyShoppingBag);
            await _context.SaveChangesAsync();
            //return false;
        }
        public async Task<ShoppingBagModel> GetShoppingBagByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var shoppingBag = await _context.ShoppingBags
                .FirstOrDefaultAsync(m => m.Id == id);
            return shoppingBag;
        }
        public async Task<ShoppingBagModel> GetShoppingBagWithDataByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var shoppingBag = await _context.ShoppingBags
                .Include(x => x.IdentityUser)
                .Include(x => x.myShoppingItems)
                .ThenInclude(x => x.myProduct)//theninclude gaat een niveau dieper, kijkt naar myShoppingItems
                .FirstOrDefaultAsync(x => x.Id == id);
            return shoppingBag;
        }
        public async Task<bool> DeleteConfirmedAsync(int? id)
        {
            if (id != null)
            {
                var shoppingBag = await GetShoppingBagByIdAsync(id);

                ///eerst de items uit de bag verwijderen
                if (shoppingBag != null)
                {
                    shoppingBag.myShoppingItems = _context.ShoppingItems.Where(x => x.myShoppingBagId == id).ToList();
                    foreach (var item in shoppingBag.myShoppingItems)
                    {
                        _context.ShoppingItems.Remove(item);
                    }
                    _context.ShoppingBags.Remove(shoppingBag);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<string> GetShoppingBagUserByIdAsync(int? id)
        {
            string result = "";
            if (id == null)
            {
                return null;
            }
            var shoppingBag = await _context.ShoppingBags
                .FirstOrDefaultAsync(m => m.Id == id);
            result = shoppingBag.IdentityUserId;
            return result;
        }
        public int AddItem(IdentityUser user, int myProductId, int ProductCategory, int myQuantity)
        {
            ShoppingBagModel myShoppingBag = _context.ShoppingBags.OrderByDescending(x => x.Date).FirstOrDefault(x => x.IdentityUserId == user.Id);///shoppingbag ophalen op basis van persoon (laatst aangemaakte)
            //ShoppingBag myShoppingBag = _context.ShoppingBags.LastOrDefault(x => x.myCustomerId == myCustomerId);//.LastOrDefault();

            ShoppingItemModel myShoppingitem = new ShoppingItemModel();
            myShoppingitem.myProductid = myProductId;
            myShoppingitem.ProductCategory = ProductCategory;
            myShoppingitem.myShoppingBagId = myShoppingBag.Id;
            myShoppingitem.Quantity = myQuantity;//TODO

            _context.ShoppingItems.Add(myShoppingitem);


            _context.SaveChanges();
            return myShoppingBag.Id;
        }
        public async Task RemoveItem(int myItemId)
        {
            ShoppingItemModel myShoppingItem = await _context.ShoppingItems.FindAsync(myItemId);
            _context.ShoppingItems.Remove(myShoppingItem);
            _context.SaveChanges();
        }
    }
}
