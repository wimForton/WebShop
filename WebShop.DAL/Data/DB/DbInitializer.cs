using WebShop.DAL.Data;
using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WebShop.DAL.Data.DB;
using WebShop.DAL.Data.Repositories.Account;

using Microsoft.AspNetCore.Identity;

namespace WebShop.Data
{
    public class DbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        public DbInitializer(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public static void Initialize(WebShopContext context)
        {
            context.Database.EnsureCreated();
            if (context.Moons.Any() || context.Planets.Any() || context.Stars.Any())
            {
                return;   // DB has been seeded
            }
            CreateSomeSpaceObjects(context);
            //CreateSomeUsers(context);


        }

        private static async Task CreateSomeUsers(WebShopContext context, UserManager<IdentityUser> _userManager)
        {
            var user = new IdentityUser { UserName = "Jan", PasswordHash = "Jan12345", Email = "jan@jan.be" };
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                //Hier maken we de default shoppingbag en voegen ze toe aan de db
                context.ShoppingBags.Add(new ShoppingBagModel { IdentityUserId = user.Id, Date = System.DateTime.Now });
                context.SaveChanges();
            }

        }

        private static void CreateSomeSpaceObjects(WebShopContext context)
        {
            List<string> moonNames = LoadNames("H:/cursus_informatica/aspdotcore/WebShop/WebShop/moonNames.txt");
            List<string> planetNames = LoadNames("H:/cursus_informatica/aspdotcore/WebShop/WebShop/planetNames.txt");
            List<string> starNames = LoadNames("H:/cursus_informatica/aspdotcore/WebShop/WebShop/starNames.txt");
            /////////////////////////////////////////////Do something with generic types!!!!! :-)
            FillSpaceObjectList<MoonModel> MoonListObj = new FillSpaceObjectList<MoonModel>("Moon", moonNames);
            FillSpaceObjectList<PlanetModel> PlanetListObj = new FillSpaceObjectList<PlanetModel>("Planet", planetNames);
            FillSpaceObjectList<StarModel> StarListObj = new FillSpaceObjectList<StarModel>("Star", starNames);
            Random myRandom = new Random();
            foreach (var moon in MoonListObj.mySpaceObjectList)
            {
                moon.OrbitalSpeed = myRandom.NextDouble() * 300 + 2;
                context.Moons.Add(moon);
            }

            foreach (var planet in PlanetListObj.mySpaceObjectList)
            {
                if (myRandom.NextDouble() > 0.75)
                {
                    planet.AnimalLife = true;
                    planet.EcoSystems = myRandom.Next(1, 150);
                }
                else
                {
                    planet.AnimalLife = false;
                    planet.EcoSystems = 0;
                }
                context.Planets.Add(planet);
            }

            foreach (var star in StarListObj.mySpaceObjectList)
            {
                star.Brightness = myRandom.Next(2000, 150000);
                context.Stars.Add(star);
            }
            context.SaveChanges();
        }
        private static List<string> LoadNames(string FilePath) 
        {
            List<string> result = new List<string>();
            using (StreamReader streamReader = new StreamReader(FilePath))
            {

                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    result.Add(line);



                }
                //MessageBox.Show(Convert.ToString(Points.Count));
            }
            return result;
        }
    }

}


