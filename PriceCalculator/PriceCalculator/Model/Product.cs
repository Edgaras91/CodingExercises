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
        public string GetProductTaxMessage(decimal tax)
        {
            decimal taxPrice = Price + (Price / 100 * tax);
            return String.Format("Product price reported as ${0} before tax and ${1} after {2}% tax.", Price.ToString("N2"), taxPrice.ToString("N2"), tax.ToString("0.##"));
        }
    }
}
