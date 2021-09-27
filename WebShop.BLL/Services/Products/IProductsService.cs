using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.BLL.Services.Products
{
    public interface IProductsService
    {
        public List<int> CountProducts();
        public int CountProductsAllFiltered();
        public IEnumerable<ProductModel> GetProducts();
        public List<ProductModel> GetProductsPage(int advancepage);
        public List<ProductModel> GetPageBySearchString(string searchString, int pagenumber, bool moons, bool planets, bool stars);
        public Task<ProductModel> GetProductByIdAsync(int? id);
        public Task<MoonModel> GetMoonByIdAsync(int? id);
        public Task<PlanetModel> GetPlanetByIdAsync(int? id);
        public Task<StarModel> GetStarByIdAsync(int? id);
        public int GetPageNumber();
        public Task AddProductAsync(ProductModel product);
        public Task UpdateProductAsync(ProductModel product);
        public Task RemoveProductAsync(int? id);
        public bool ProductExists(int id);
    }
}
