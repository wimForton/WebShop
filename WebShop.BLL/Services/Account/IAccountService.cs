using WebShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.BLL.Services.Account
{
    public interface IAccountService
    {
        public IEnumerable<IdentityUser> GetAccounts();
        public Task<IdentityUser> GetAccountByIdAsync(string id);
        public Task<IdentityUser> GetAccountByNameAsync(string name);
        public List<ShoppingBagModel> GetShoppingBagsByAccount(IdentityUser user);
        public Task<bool> DeleteShoppingBagsByAccountAsync(IdentityUser user);
        public Task<bool> RegisterUserAsync(LoginViewModel loginViewModel);
        public Task<bool> DeleteUserAsync(string id);
    }
}
