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

        public string GetProductPriceMessage(decimal taxPrecent, bool productDiscountBeforeTax = false)
        {
            decimal taxedPrice = 0;
            decimal globalDiscount = 0;
            decimal productDiscount = 0;
            decimal finalPrice = 0;

            if (Program.Discounts.ProductDiscounts.ContainsKey(UPC))
            {
                if (productDiscountBeforeTax == true)
                {
                    decimal UPCDiscount = Price.DiscountPrice(Program.Discounts.ProductDiscounts[UPC]);
                    decimal remainingPrice = (Price - UPCDiscount).Round();

                    decimal taxprice = remainingPrice.DiscountPrice(taxPrecent);
                    taxedPrice = Price + taxprice;
                    decimal universalDiscount = remainingPrice.DiscountPrice(Program.Discounts.GlobalDiscountPrecent);

                    finalPrice = (Price - UPCDiscount + taxprice - universalDiscount).Round();

                }
                else
                {
                    if (Program.Discounts.GlobalDiscountPrecent != 0)
                    {
                        globalDiscount = Price.DiscountPrice(Program.Discounts.GlobalDiscountPrecent);
                    }
                    taxedPrice = Price + (Price / 100 * taxPrecent);

                    finalPrice = (taxedPrice - (globalDiscount + productDiscount)).Round();
                }
            }

            decimal discount = (taxedPrice - finalPrice).Round();
            return String.Format("Final price is ${0}, total discount is ${1} ", finalPrice, discount);
        }



    }
}
