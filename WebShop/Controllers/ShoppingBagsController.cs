using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.BLL.Services.Products;
using WebShop.BLL.Services.ShoppingBags;
using WebShop.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingBagsController : ControllerBase
    {
        private readonly IShoppingBagsService _ShoppingBagsService;
        private readonly IProductsService _ProductsService;
        private readonly UserManager<IdentityUser> _userManager;
        public ShoppingBagsController(IShoppingBagsService shoppingBagsService, IProductsService productsService,UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _ShoppingBagsService = shoppingBagsService;
            _ProductsService = productsService;
        }

        //private readonly IProductService
        // GET: api/<ProductsController>
        //[HttpGet]
        //public IEnumerable<ProductModel> Get()
        //{

        //    //return new string[] { "value1", "value2", "test" };
        //    try
        //    {
        //        return _ShoppingBagsService.
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ShoppingBagModel> Get(int id)
        {
            return await _ShoppingBagsService.GetShoppingBagByIdAsync(id);
        }
        [HttpGet("BagUserByName/{username}")]
        public async Task<List<int>> GetShoppingItemsByUserAsync(string username)
        {
            //json settings in startup: referenceHandler.Preserve om loop te stoppen
            //loop removed door enkel de nodige parameters in een list te steken, hackerdehack, dees was kak...
            //List<int> itemsList = await _ShoppingBagsService.GetShoppingItemsByUserAsync(username);

            return await _ShoppingBagsService.GetShoppingItemsByUserAsync(username);
        }

        [HttpGet("ProductsByUserName/{username}")]
        public async Task<List<ProductModel>> GetShoppingProductsByUserAsync(string username)
        {

            List<int> itemsList = await _ShoppingBagsService.GetShoppingItemsByUserAsync(username);
            List<ProductModel> productsList = new List<ProductModel>();

            for (int i = 0; i < itemsList.Count; i = i + 2)
            {
                if(itemsList[i] == 1)productsList.Add(await _ProductsService.GetMoonByIdAsync(i+1) as ProductModel);
                if (itemsList[i] == 2) productsList.Add(await _ProductsService.GetPlanetByIdAsync(i + 1) as ProductModel);
                if (itemsList[i] == 3) productsList.Add(await _ProductsService.GetStarByIdAsync(i + 1) as ProductModel);
            }
            var test = productsList;

            return productsList;
        }

        [HttpPost("AddItem/{myProductId}/{ProductCategory}/{UserName}")]
        public async Task<int> AddItem(int myProductId, int ProductCategory, string UserName)
        {
            int myQuantity = 1;
            //string test = User.Identity.Name;
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = await _userManager.FindByNameAsync(UserName);
            return _ShoppingBagsService.AddItem(user, myProductId, ProductCategory, myQuantity);
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
