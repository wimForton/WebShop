using WebShop.DAL.Data.Repositories.Product;
using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.BLL.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _ProductRepository;
        public ProductsService(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }
        public List<int> CountProducts()
        {
            return _ProductRepository.CountProducts();
        }
        public int CountProductsAllFiltered()
        {
            return _ProductRepository.CountProductsAllFiltered();
        }
        public IEnumerable<ProductModel> GetProducts()
        {
            return _ProductRepository.GetProducts();
        }
        public List<ProductModel> GetProductsPage(int advancepage)
        {
            return _ProductRepository.GetProductsPage(advancepage);
        }
        public List<ProductModel> GetPageBySearchString(string searchString, int pagenumber, bool moons, bool planets, bool stars)
        {
            return _ProductRepository.GetPageBySearchString(searchString, pagenumber, moons, planets, stars);
        }
        public async Task<ProductModel> GetProductByIdAsync(int? id)
        {
            return await _ProductRepository.GetProductByIdAsync(id);
        }
        public async Task<MoonModel> GetMoonByIdAsync(int? id)
        {
            return await _ProductRepository.GetMoonByIdAsync(id);
        }
        public async Task<PlanetModel> GetPlanetByIdAsync(int? id)
        {
            return await _ProductRepository.GetPlanetByIdAsync(id);
        }
        public async Task<StarModel> GetStarByIdAsync(int? id)
        {
            return await _ProductRepository.GetStarByIdAsync(id);
        }
        public int GetPageNumber()
        {
            return _ProductRepository.GetPageNumber();
        }
        public async Task AddProductAsync(ProductModel product)
        {
            await _ProductRepository.AddProductAsync(product);
        }
        public async Task UpdateProductAsync(ProductModel product)
        {
            await _ProductRepository.UpdateProductAsync(product);
        }
        public async Task RemoveProductAsync(int? id)
        {
            await _ProductRepository.RemoveProductAsync(id);
        }
        public bool ProductExists(int id)
        {
            return _ProductRepository.ProductExists(id);
        }
    }
}
