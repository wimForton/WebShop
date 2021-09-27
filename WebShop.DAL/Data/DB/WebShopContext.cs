using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.DAL.Data
{
    public class WebShopContext:IdentityDbContext<IdentityUser>
    {
        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options)
        {
        }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<ProductModel> Products { get; set; }
        public DbSet<MoonModel> Moons { get; set; }
        public DbSet<PlanetModel> Planets { get; set; }
        public DbSet<StarModel> Stars { get; set; }
        public DbSet<ShoppingBagModel> ShoppingBags { get; set; }
        public DbSet<ShoppingItemModel> ShoppingItems { get; set; }


    }
}
