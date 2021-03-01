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
        private static decimal Tax = 21;
        private static List<Product> Products;
        internal static Discounts Discounts;
        private static void Main(string[] args)
        {
            //Lets begin!
            Initialise();


            foreach (Product product in Products)
            {
                Console.WriteLine(product.GetProductInfo());
            }

            Console.ReadLine();
        }

        private static void Initialise()
        {
            Discounts = new Discounts { GlobalDiscountPrecent = 15, ProductDiscounts = new Dictionary<int, decimal>() { [12345] = 7 } };


            Products = new List<Product>()
            {
                new Product("The little Prince", 12345, 20.25m, Tax, 1, 2.2m ),
                new Product("The little Prince", 6789, 20.25m, Tax, discountEnabled: false )
            };

        }

    }
}
