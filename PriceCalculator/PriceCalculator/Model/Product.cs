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

        public string GetProductPriceMessage(decimal taxPrecent)
        {
            decimal taxPrice = Price + (Price / 100 * taxPrecent);
            decimal globalDiscount = 0;
            decimal productDiscount = 0;

            if(Program.Discounts.GlobalDiscountPrecent != 0)
            {
                globalDiscount = Price / 100 * Program.Discounts.GlobalDiscountPrecent;
            }

            if (Program.Discounts.ProductDiscounts.ContainsKey(UPC))
            {
                productDiscount = (Price / 100 * Program.Discounts.ProductDiscounts[UPC]);
            }

            decimal finalPrice = taxPrice - (globalDiscount + productDiscount);
            decimal discount = taxPrice - finalPrice;
            return String.Format("Final price is ${0}, total discount is ${1} ", Math.Round(finalPrice, 2), Math.Round(discount, 2));
        }

    }
}
