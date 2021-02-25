﻿using PriceCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    class Program
    {
        private static decimal Tax = 20;
        private static List<Product> Products;
        private static void Main(string[] args)
        {
            //Lets begin!
            Initialise();
            Console.WriteLine(Products[0].GetProductPriceMessage(Tax));
            Console.WriteLine(Products[0].GetProductPriceMessage(Tax + 1));
            Console.WriteLine(Products[0].GetProductPriceMessage(Tax, 15));

            Console.ReadLine();
        }

        private static void Initialise()
        {
            Products = new List<Product>() { new Product { Name = "The little Prince", Price = 20.25m, UPC = 12345 } };
        }

    }
}