using WebShop.DAL.Data;
using WebShop.DAL.Data.Repositories.Account;
using WebShop.DAL.Data.Repositories.Product;
using WebShop.DAL.Data.Repositories.ShoppingBag;
using WebShop.DAL.Data.Repositories.SiteConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebShop.DAL
{
    public static class DALExtension
    {
        public static IServiceCollection RegisterDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebShopContext>(options => options.UseSqlServer(configuration.GetConnectionString("WebShopConnection")));


            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IShoppingBagRepository, ShoppingBagRepository>();
            services.AddTransient<ISiteConfigRepository, SiteConfigRepository>();


            //.AddSignInManager<SignInManager<IdentityUser>>();//testje

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;

            });

            return services;
        }
    }
}
