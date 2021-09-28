using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.DAL.Data.DB
{
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
