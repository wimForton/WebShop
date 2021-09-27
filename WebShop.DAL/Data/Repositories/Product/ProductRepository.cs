using WebShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DAL.Data.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        public static int PageNumber = 1;
        public static int ProductsPerPage = 9;
        public static int FilterType = 1;// 1 = all, 2 = via searchstring, 3 = category A, 4 = category B....
        public static bool ShowMoons = true;
        public static bool ShowPlanets = true;
        public static bool ShowStars = true;
        //public static int NumberOfMoons = 0;
        //public static int NumberOfPlanets = 0;
        //public static int NumberOfStars = 0;
        public static bool FilterHasChanged = true;
        public static int ProductsAllFiltered = 0;
        private readonly WebShopContext _context;
        public ProductRepository(WebShopContext context)
        {
            _context = context;
        }
        public List<int> CountProducts() {
            List<int> productsCount = new List<int>();
            productsCount.Add(_context.Moons.Count());
            productsCount.Add(_context.Planets.Count());
            productsCount.Add(_context.Stars.Count());
            return productsCount;
        }
        public int CountProductsAllFiltered()
        {
            return ProductRepository.ProductsAllFiltered;
        }
        public async Task<IQueryable<ProductModel>> GetProductsAllCategorysAsync()//async versie
        {
            List<ProductModel> products = new List<ProductModel>();
            //var products;
            var moons = await _context.Moons.Include(x => x.Discounts).ToListAsync();
            var Planets = await _context.Planets.Include(x => x.Discounts).ToListAsync();
            var Stars = await _context.Stars.Include(x => x.Discounts).ToListAsync();
            products.AddRange(moons);
            products.AddRange(Planets);
            products.AddRange(Stars);
            return products.AsQueryable();
        }
        public List<ProductModel> GetProductsAllCategorys()
        {
            List<ProductModel> products = new List<ProductModel>();
            var moons = _context.Moons.Include(x => x.Discounts);  // as MoonModel;/// morge jesse vrage
            var Planets = _context.Planets.Include(x => x.Discounts);
            var Stars = _context.Stars.Include(x => x.Discounts);
            products.AddRange(moons);
            products.AddRange(Planets);
            products.AddRange(Stars);
            return products;
        }
        public IEnumerable<ProductModel> GetProducts()
        {
            var products = GetProductsAllCategorys();//_context.Products.Include(x => x.Discounts);

            return products;
        }
        public async Task<ProductModel> GetProductByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var product = await GetProductsAllCategorysAsync();
            var result = product.FirstOrDefault(m => m.Id == id);
            return result;
        }
        public async Task<MoonModel> GetMoonByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var moons = await _context.Moons.Include(x => x.Discounts).ToListAsync();
            var product = moons.FirstOrDefault(m => m.Id == id);
            return product;
        }
        public async Task<PlanetModel> GetPlanetByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var moons = await _context.Planets.Include(x => x.Discounts).ToListAsync();
            var product = moons.FirstOrDefault(m => m.Id == id);
            return product;
        }
        public async Task<StarModel> GetStarByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var moons = await _context.Stars.Include(x => x.Discounts).ToListAsync();
            var product = moons.FirstOrDefault(m => m.Id == id);
            return product;
        }
        public bool ProductExists(int id)
        {
            return GetProductsAllCategorys().Any(e => e.Id == id);
        }
        public async Task AddProductAsync(ProductModel product)
        {
            _context.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var x = e.Message;
                //Message.box(x);
                throw;
            }
        }
        public async Task RemoveProductAsync(int? id)///gaan we niet doen, enkel inactive zetten, anders ook uit de shoppingbags verwijderen etc.
        {
            ProductModel product = await GetProductByIdAsync(id);
            //try
            //{
            //    _context.Products.Remove(product);
            //    await _context.SaveChangesAsync();
            //}
            //catch (Exception e)
            //{
            //    var x = e.Message;
            //    throw;
            //}
        }
        public async Task UpdateProductAsync(ProductModel product)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var x = e.Message;
                //Message.box(x);
                throw;
            }
        }
        public List<ProductModel> GetProductsPage(int pagenumber)
        {
            var products = GetProductsAllCategorys();
            if (ProductRepository.FilterHasChanged)
            {
                ProductRepository.PageNumber = 1;
                ProductRepository.FilterHasChanged = false;
            }
            ProductRepository.PageNumber = pagenumber;
            if (ProductRepository.PageNumber < 1)
            {
                ProductRepository.PageNumber = 1;
            }
            if (ProductRepository.PageNumber > products.Count() / ProductRepository.ProductsPerPage + 1)
            {
                ProductRepository.PageNumber = products.Count() / ProductRepository.ProductsPerPage + 1;
            }
            return products.Skip((ProductRepository.PageNumber - 1) * ProductRepository.ProductsPerPage).Take(ProductRepository.ProductsPerPage).ToList();
        }
        //GetPageBySearchString
        public List<ProductModel> GetPageBySearchString(string searchString, int pagenumber, bool moons, bool planets, bool stars)
        {

            var products = new List<ProductModel>();
            if (moons) 
            {
            var moonList = _context.Moons.Include(x => x.Discounts);  // as MoonModel;/// morge jesse vrage
            products.AddRange(moonList);
            }
            if (planets)
            {
            var PlanetList = _context.Planets.Include(x => x.Discounts);
            products.AddRange(PlanetList);
            }
            if (stars)
            {
            var StarList = _context.Stars.Include(x => x.Discounts);
            products.AddRange(StarList);
            }
            if (searchString != "all") {
                products = products.Where(x => x.Name.Contains(searchString)).ToList();
            }
            ProductRepository.ProductsAllFiltered = products.Count();

            if (ProductRepository.FilterHasChanged)
            {
                ProductRepository.PageNumber = 1;
                ProductRepository.FilterHasChanged = false;
            }
            ProductRepository.PageNumber = pagenumber;
            if (ProductRepository.PageNumber < 1)
            {
                ProductRepository.PageNumber = 1;
            }
            if (ProductRepository.PageNumber > products.Count() / ProductRepository.ProductsPerPage + 1)
            {
                ProductRepository.PageNumber = products.Count() / ProductRepository.ProductsPerPage + 1;
            }
            return products.Skip((ProductRepository.PageNumber - 1) * ProductRepository.ProductsPerPage).Take(ProductRepository.ProductsPerPage).ToList();
        }
        public int GetPageNumber()
        {
            return ProductRepository.PageNumber;
        }
    }
}
