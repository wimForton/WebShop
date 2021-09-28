using WebShop.BLL.Services.Account;
using WebShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAccountService _accountService;
        public AccountController(IConfiguration configuration, UserManager<IdentityUser> userManager, IAccountService accountService)
        {
            _accountService = accountService;
            _configuration = configuration;
            _userManager = userManager;
        }
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (login == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44310",
                    audience: "http://localhost:44310",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { token = tokenString, user = user });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] LoginViewModel model)///check frombody works
        {

            //var user = new IdentityUser { UserName = model.UserName, PasswordHash = model.Password, Email = model.Email };
            //var result = _userManager.CreateAsync(user, model.Password);
            //if (!result.Result.Succeeded)
            //{
            //    return BadRequest(result.Result.Errors);
            //}
            await _accountService.RegisterUserAsync(model);


            return Ok();
        }
        [HttpGet("getUserBags/{userName}")]
        public async Task<IEnumerable<ShoppingBagModel>> GetShoppingBagsCurrentAccount(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return _accountService.GetShoppingBagsByAccount(user);
        }
        [HttpGet("getLastUserBag/{userName}")]
        public async Task<ShoppingBagModel> GetLastShoppingBagCurrentAccount(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return _accountService.GetShoppingBagsByAccount(user).ToList().Last();
        }
    }
}
