using WebShop.DAL.Data.Repositories.Account;
using WebShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _AccountRepository;
        public AccountService(IAccountRepository AccountRepository)
        {
            _AccountRepository = AccountRepository;
        }
        public Task<bool> DeleteShoppingBagsByAccountAsync(IdentityUser user)
        {
            return _AccountRepository.DeleteShoppingBagsByAccountAsync(user);
        }

        public Task<bool> DeleteUserAsync(string id)
        {
            return _AccountRepository.DeleteUserAsync(id);
        }

        public Task<IdentityUser> GetAccountByIdAsync(string id)
        {
            return _AccountRepository.GetAccountByIdAsync(id);
        }

        public Task<IdentityUser> GetAccountByNameAsync(string name)
        {
            return _AccountRepository.GetAccountByNameAsync(name);
        }

        public IEnumerable<IdentityUser> GetAccounts()
        {
            return _AccountRepository.GetAccounts();
        }

        public List<ShoppingBagModel> GetShoppingBagsByAccount(IdentityUser user)
        {
            return _AccountRepository.GetShoppingBagsByAccount(user);
        }

        public Task<bool> RegisterUserAsync(LoginViewModel loginViewModel)
        {
            return _AccountRepository.RegisterUserAsync(loginViewModel);
        }
    }
}
