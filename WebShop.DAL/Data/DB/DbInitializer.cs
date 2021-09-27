using WebShop.DAL.Data;
using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebShop.Data
{
    public class DbInitializer
    {
        public static void Initialize(WebShopContext context)
        {
            context.Database.EnsureCreated();
            if (context.Moons.Any() || context.Planets.Any() || context.Stars.Any())
            {
                return;   // DB has been seeded
            }

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
    public class FillSpaceObjectList<T1> where T1 : ProductModel, new()
    {
        public List<T1> mySpaceObjectList = new List<T1>();

        //constructor
        public FillSpaceObjectList(string preFix, List<string> SpaceObjectNames)
        {
            CreateList(preFix, SpaceObjectNames);
        }
        public void CreateList(string preFix, List<string> SpaceObjectNames)
        {
            Random myRandom = new Random();
            for (int i = 0; i < SpaceObjectNames.Count(); i++)
            {
                string myName = "misteryObject";
                if (SpaceObjectNames.Count() > i + 1)
                {
                    myName = SpaceObjectNames[i];
                }
                double orbit = myRandom.NextDouble() * 300 + 2;
                int price = myRandom.Next(2000, 15000);
                string frameDigits = Digits(i);
                T1 mySpaceObject = new T1();
                mySpaceObject.Imagepath = $"images/{preFix}{frameDigits}.png";
                mySpaceObject.Price = price;
                mySpaceObject.Name = $"{myName}";
                mySpaceObject.Available = true;

                if (mySpaceObject.GetType() == typeof(MoonModel))
                {
                    mySpaceObject.ProductCategory = 1;
                    mySpaceObject.SerialNumber = "MN_" + Convert.ToString(myRandom.Next(100000, 999999)) + "_" + Convert.ToString(i);
                }
                if (mySpaceObject.GetType() == typeof(PlanetModel))
                {
                    mySpaceObject.ProductCategory = 2;
                    mySpaceObject.SerialNumber = "PL_" + Convert.ToString(myRandom.Next(100000, 999999)) + "_" + Convert.ToString(i);
                }
                if (mySpaceObject.GetType() == typeof(StarModel))
                {
                    mySpaceObject.ProductCategory = 3;
                    mySpaceObject.SerialNumber = "ST_" + Convert.ToString(myRandom.Next(100000, 999999)) + "_" + Convert.ToString(i);
                }
                mySpaceObjectList.Add(mySpaceObject);
            }
            //return inList;
        }
        private static string Digits(int number)
        {
            int i = number;
            string frameDigits = "";
            if (i < 9)
            {
                frameDigits = $"000{i + 1}";
            }
            else if (i < 99)
            {
                frameDigits = $"00{ i + 1}";
            }
            else
            {
                frameDigits = $"0{i + 1}";
            }
            return frameDigits;
        }

    }

}


