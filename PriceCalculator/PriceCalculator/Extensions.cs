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
        /// <param name="precentage"></param>
        /// <returns></returns>
        public static decimal Precentage(this decimal amount, decimal precentage)
        {
            return (amount / 100 * precentage).Round(Program.DecimalPointsRounding);
        }
        public static decimal Round(this decimal amount, int decimalPoints)
        {
            return Math.Round(amount, decimalPoints);
        }
    }
}
