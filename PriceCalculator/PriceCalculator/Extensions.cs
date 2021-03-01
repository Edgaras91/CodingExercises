using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public static class Extensions
    {
        /// <summary>
        /// Returns Discount price
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="discountPrecent"></param>
        /// <returns></returns>
        public static decimal Precentage(this decimal amount, decimal discountPrecent)
        {
            return (amount / 100 * discountPrecent).Round();
        }
        public static decimal Round(this decimal amount)
        {
            return Math.Round(amount, 2);
        }
    }
}
