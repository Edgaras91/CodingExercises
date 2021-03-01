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
        public string Currency { get; set; }
        public int UPC { get; }
        public decimal Price { get; set; }
        public decimal TaxPrecent { get; set; }
        public decimal TaxAmount { get => Price.Precentage(TaxPrecent); }
        public decimal Discounts { get; }
        public decimal PackagingPrecent { get; set; }
        public decimal PackagingAmount { get => Price.Precentage(PackagingPrecent); }
        public decimal TransportAmount { get; set; }
        public decimal TransportPrecent { get; set; } 
        public decimal TOTAL { get; set; }
        public decimal UPCDiscount { get => Program.Discounts.ProductDiscounts.ContainsKey(UPC) == true && DiscountEnabled ? Program.Discounts.ProductDiscounts[UPC] : 0; }
        public bool DiscountEnabled { get; set; } = true;
        public decimal CapAmount { get; set; }
        public decimal CapPrecentage { get; set; }
        public int DecimalPointsRounding { get; set; } = 4;//In Calculations Only

        //issue: when property changes (price), we need to run update other values as per constructor.
        //Alternativeley we call calculation methods in getters.
        public Product(string name, int upc, decimal price, string currency, decimal tax, decimal packagingPrecent = 0, decimal transportAmount = 0, decimal transportPrecent = 0, decimal capPrecentage = 0, decimal capAmount = 0, bool discountEnabled = true, bool discountMultiplicative = false)
        {
            Name = name;
            UPC = upc;
            Price = price;
            Currency = currency;
            TaxPrecent = tax;
            PackagingPrecent = packagingPrecent;
            TransportAmount = transportAmount;
            TransportPrecent = transportPrecent;
            CapPrecentage = capPrecentage;
            CapAmount = capAmount;

            if (TransportPrecent != 0)
                TransportAmount = Price.Precentage(TransportPrecent);

            if (discountEnabled)
                Discounts = CalculateDiscounts(discountMultiplicative);

            TOTAL = (Price + TaxAmount - Discounts + PackagingAmount + TransportAmount).Round(Program.DecimalPointsRounding);

        }

        private decimal CalculateDiscounts(bool discountMultiplicative = false)
        {
            decimal tempCap = 0;
            decimal discount = 0;

            //CalculateCapAmount
            if (CapPrecentage != 0)
                tempCap = Price.Precentage(CapPrecentage);

            if (CapPrecentage == 0 && CapAmount != 0)
            {
                tempCap = CapAmount;
            }

            //CalculateDiscount
            if (discountMultiplicative == true)
            {
                decimal discount1 = Price.Precentage(Program.Discounts.GlobalDiscountPrecent);
                decimal discount2 = (Price - discount1).Precentage(UPCDiscount);
                discount = (discount1 + discount2).Round(Program.DecimalPointsRounding);
            }
            else
            {
                   discount = (Price.Precentage(Program.Discounts.GlobalDiscountPrecent)) + (Price.Precentage(UPCDiscount)).Round(Program.DecimalPointsRounding);
            }

            if (tempCap != 0 && discount > tempCap)
                return tempCap;
            else
                return discount;

        }


        public string GetProductInfo()
        {
            string output = string.Empty;

            output += "========Product UPC: " + UPC + "========";

            output += Environment.NewLine + "Cost = " + Price.Round(Program.FinalPriceRounding) + " " + Currency;

            output += Environment.NewLine + "Tax = " + TaxAmount.Round(Program.FinalPriceRounding) + " " + Currency;

            if (Discounts != 0)
                output += Environment.NewLine + "Discounts = " + Discounts.Round(Program.FinalPriceRounding) + " " + Currency;

            if (PackagingAmount != 0)
                output += Environment.NewLine + "Packaging = " + PackagingAmount.Round(Program.FinalPriceRounding) + " " + Currency;

            if (TransportAmount != 0)
                output += Environment.NewLine + "Transport = " + TransportAmount.Round(Program.FinalPriceRounding) + " " + Currency;

            output += Environment.NewLine + "TOTAL = " + TOTAL.Round(Program.FinalPriceRounding) + " " + Currency;

            if (Discounts != 0)
                output += Environment.NewLine + "Total Discount = " + Discounts.Round(Program.FinalPriceRounding) + " " + Currency;
            else
                output += Environment.NewLine + "No discounts";

            output += Environment.NewLine;//End space

            return output;
        }
    }
}
