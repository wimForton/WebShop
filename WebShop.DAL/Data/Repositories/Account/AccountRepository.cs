using WebShop.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DAL.Data.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WebShopContext _context;
        public AccountRepository(UserManager<IdentityUser> userManager, WebShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IEnumerable<IdentityUser> GetAccounts()
        {
            return _context.Users;
        }
        public async Task<IdentityUser> GetAccountByIdAsync(string id)
        {
            if (id == null)
            {
                return null;
            }
            var account = await _context.Users.FindAsync(id);
            return account;
        }
        public async Task<IdentityUser> GetAccountByNameAsync(string name)
        {
            if (name == null)
            {
                return null;
            }
            var account = await _userManager.FindByNameAsync(name);
            return account;
        }
        public List<ShoppingBagModel> GetShoppingBagsByAccount(IdentityUser user)
        {
            return _context.ShoppingBags.Where(x => x.IdentityUserId == user.Id).ToList();
        }
        public async Task<bool> DeleteShoppingBagsByAccountAsync(IdentityUser user)
        {
            bool succes = false;

            if (user != null)
            {
                ShoppingBagModel myShoppingBag = _context.ShoppingBags.FirstOrDefault(x => x.IdentityUserId == user.Id);
                while (myShoppingBag != null)
                {
                    _context.ShoppingBags.Remove(myShoppingBag);
                    await _context.SaveChangesAsync();
                    myShoppingBag = _context.ShoppingBags.FirstOrDefault(x => x.IdentityUserId == user.Id);///shoppingbag ophalen op basis van persoon
                }
                succes = true;
            }
            else
            {
                succes = false;
            }
            return succes;
        }
        public async Task<bool> DeleteUserAsync(string id)
        {
            bool succes = false;
            var account = await GetAccountByIdAsync(id);
            if(await DeleteShoppingBagsByAccountAsync(account))
            {
                _context.Users.Remove(account);
                await _context.SaveChangesAsync();
                succes = true;
            }

            return succes;
        }
        public async Task<bool> RegisterUserAsync(LoginViewModel loginViewModel)
        {
            var user = new IdentityUser { UserName = loginViewModel.UserName, PasswordHash = loginViewModel.Password, Email = loginViewModel.Email };
            var result = await _userManager.CreateAsync(user, loginViewModel.Password);

            if (result.Succeeded)
            {
                //Hier maken we de default shoppingbag en voegen ze toe aan de db
                _context.ShoppingBags.Add(new ShoppingBagModel { IdentityUserId = user.Id, Date = System.DateTime.Now });
                _context.SaveChanges();
                return true;
            }
            else
            {
                //what if the user couldn't be added?
                //logging
                //exception handling
                //...
            }
            return false;
        }
    }
}
