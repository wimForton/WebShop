using WebShop.DAL;
using WebShop.BLL.Services.Account;
using WebShop.BLL.Services.Products;
using WebShop.BLL.Services.ShoppingBags;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.BLL
{
    public static class BLLExtension
    {
        public static IServiceCollection RegisterBLL(this IServiceCollection services, IConfiguration configuration)
        {

            services.RegisterDAL(configuration);
            //services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IShoppingBagsService, ShoppingBagsService>();

            return services;
        }
    }
}
