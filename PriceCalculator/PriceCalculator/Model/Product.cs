using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator.Model
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public decimal Price { get; set; }
        //I would put tax, discounts in getters and setters.
        //Also on get, I would math.round.
        public string GetProductPriceMessage(decimal tax)
        {
            decimal taxPrice = Price + (Price / 100 * tax);
            return String.Format("Product price reported as ${0} before tax and ${1} after {2}% tax.", Price.ToString("N2"), taxPrice.ToString("N2"), tax.ToString("0.##"));
        }
        public string GetProductPriceMessage(decimal taxPrecent, decimal discountPrecent)
        {
            decimal taxPrice = Price + (Price / 100 * taxPrecent);
            decimal finalPrice = taxPrice - (Price / 100 * discountPrecent);
            decimal discount = taxPrice - finalPrice;
            return String.Format("Original price is ${0}, after {1}% tax and discounted {2}% resulting in final product price ${3}, discounted by ${4}", Price, taxPrecent,discountPrecent, finalPrice.ToString("N2"), discount.ToString("N2"));
        }

    }
}
