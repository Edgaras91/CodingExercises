using PriceCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    class Program
    {
        private static decimal Tax = 0;
        private static List<Product> Products;
        internal static Discounts Discounts;
        private static void Main(string[] args)
        {
            //Lets begin!
            Initialise();

            Tax = 20;
            Discounts.ProductDiscounts = new Dictionary<int, decimal>() { [12345] = 7 };
            Console.WriteLine(Products[0].GetProductPriceMessage(Tax, true));

            Console.ReadLine();
        }

        private static void Initialise()
        {
            Products = new List<Product>() { new Product { Name = "The little Prince", Price = 20.25m, UPC = 12345 } };
            Discounts = new Discounts { GlobalDiscountPrecent = 15, };
        }

    }
}
