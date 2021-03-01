﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator.Model
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; }
        public decimal Price { get; set; }
        public decimal TaxPrecent { get; set; }
        public decimal TaxAmount { get => Price.Precentage(TaxPrecent); }
        public decimal Discounts { get; }
        public decimal PackagingPrecent { get; set; }
        public decimal PackagingAmount { get => Price.Precentage(PackagingPrecent); }
        public decimal Transport { get; set; }
        public decimal TOTAL { get; set; }
        public decimal UPCDiscount { get => Program.Discounts.ProductDiscounts.ContainsKey(UPC) == true && DiscountEnabled ? Program.Discounts.ProductDiscounts[UPC] : 0; }
        public bool DiscountEnabled { get; set; } = true;

        //issue: when property changes (price), we need to run update other values as per constructor.
        //Alternativeley we call calculation methods in getters.
        public Product(string name, int upc, decimal price, decimal tax, decimal packagingPrecent = 0, decimal transport = 0, bool discountEnabled = true, bool discountMultiplicative = false)
        {
            Name = name;
            UPC = upc;
            Price = price;
            TaxPrecent = tax;
            PackagingPrecent = packagingPrecent;
            Transport = transport;

            if (discountEnabled)
                Discounts = CalculateDiscounts(discountMultiplicative);

            TOTAL = (Price + TaxAmount - Discounts + PackagingAmount + Transport).Round();

        }

        private decimal CalculateDiscounts(bool discountMultiplicative = false)
        {
            if (discountMultiplicative == true)
            {
                decimal discount1 = Price.Precentage(Program.Discounts.GlobalDiscountPrecent);
                decimal discount2 = (Price - discount1).Precentage(UPCDiscount);
                return (discount1 + discount2).Round();
            }
            else
            {
                return (Price.Precentage(Program.Discounts.GlobalDiscountPrecent)) + (Price.Precentage(UPCDiscount)).Round();
            }

        }


        public string GetProductInfo()
        {
            string output = string.Empty;

            output += "========Product UPC: " + UPC + "========";

            output += Environment.NewLine + "Cost = $" + Price;

            output += Environment.NewLine + "Tax = " + TaxAmount;

            if (Discounts != 0)
                output += Environment.NewLine + "Discounts = " + Discounts;

            if (PackagingAmount != 0)
                output += Environment.NewLine + "Packaging = " + PackagingAmount;

            if (Transport != 0)
                output += Environment.NewLine + "Transport = " + Transport;

            output += Environment.NewLine + "TOTAL = " + TOTAL;

            if (Discounts != 0)
                output += Environment.NewLine + "Total Discount = " + Discounts;
            else
                output += Environment.NewLine + "No discounts";

            output += Environment.NewLine;//End space

            return output;
        }
    }
}
