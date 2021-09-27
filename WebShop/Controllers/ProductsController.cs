using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.BLL.Services.Products;
using WebShop.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _ProductService;
        public ProductsController(IProductsService ProductService)
        {
            _ProductService = ProductService;
        }
        [HttpGet("count")]
        public int CountProductsAllFiltered()
        {
            return _ProductService.CountProductsAllFiltered();
        }
        //private readonly IProductService
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {

            //return new string[] { "value1", "value2", "test" };
            try
            {
                return _ProductService.GetProducts();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //[HttpGet("page/{pagenum}")]
        //public IEnumerable<ProductModel> GetSinglePage(int pagenum)
        //{
        //    return _ProductService.GetProductsPage(pagenum);
        //}

        //[HttpGet("page/{pagenum}/{moons}/{planets}/{stars}")]
        [HttpGet("page/{pagenum}")]
        public IEnumerable<ProductModel> GetPageBySearchString(int pagenum, bool moons = true, bool planets = true, bool stars = true, string searchString = "all")
        {

            return _ProductService.GetPageBySearchString(searchString, pagenum, moons, planets, stars);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // GET api/<ProductsController>/5
        [HttpGet("moon/{id}")]
        public async Task<MoonModel> GetMoon(int id)
        {
            return await _ProductService.GetMoonByIdAsync(id);
        }
        [HttpGet("planet/{id}")]
        public async Task<PlanetModel> GetPlanet(int id)
        {
            return await _ProductService.GetPlanetByIdAsync(id);
        }
        [HttpGet("star/{id}")]
        public async Task<StarModel> GetStar(int id)
        {
            return await _ProductService.GetStarByIdAsync(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
