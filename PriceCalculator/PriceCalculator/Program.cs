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

        //Used for calculations only, not final figure roundings.
        public static int DecimalPointsRounding { get; set; } = 4;
        public static int FinalPriceRounding { get; set; } = 2;

        private static void Main(string[] args)
        {
            Initialise();

            foreach (Product product in Products)
            {
                Console.WriteLine(product.GetProductInfo());
            }

            Console.ReadLine();

            //Bugs:
            //7. COMBINING Case 2 Total was calculated as 22.66 with default roundings to 2 decimals on all decimals.
            //9. CURRENCY Case 1 Total is calculated with 21% tax, but stated 20% (Case 2 was calculated with 20% (correct)).
            //10. PRECISION I could not get the final figure to match (everything else including discount matches).
        }

        private static void Initialise()
        {
            Discounts = new Discounts { GlobalDiscountPrecent = 15, ProductDiscounts = new Dictionary<int, decimal>() { [12345] = 7 } };

            Products = new List<Product>()
            {
                new Product("The little Prince", 12345, 20.25m, "USD", Tax, transportPrecent: 3, discountMultiplicative: true),
            };

        }

    }
}
