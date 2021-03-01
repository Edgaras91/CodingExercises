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
        private static decimal Tax = 20;
        private static List<Product> Products;
        internal static Discounts Discounts;
        private static void Main(string[] args)
        {
            Initialise();

            foreach (Product product in Products)
            {
                Console.WriteLine(product.GetProductInfo());
            }

            Console.ReadLine();
            //Bugs:
            //7. COMBINING Case 2 Total was calculated as 22.66 with default roundings to 2 decimals on all decimals
            //9. CURRENCY Case 1 Total is calculated with 21% tax, but stated 20% (Case 2 was calculated with 20% (correct))
        }

        private static void Initialise()
        {

            Discounts = new Discounts { ProductDiscounts = new Dictionary<int, decimal>() };

            Products = new List<Product>()
            {
                new Product("The little Prince", 12345, 20.25m, "USD", Tax +1), //See bug 9. in Main().
                new Product("The little Prince", 12345, 17.76m, "GBP", Tax),
            };

        }

    }
}
