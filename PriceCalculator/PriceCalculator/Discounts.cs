using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class Discounts
    {
        public decimal GlobalDiscountPrecent { get; set; }
        public Dictionary<int, decimal> ProductDiscounts { get; set; }
    }
}
