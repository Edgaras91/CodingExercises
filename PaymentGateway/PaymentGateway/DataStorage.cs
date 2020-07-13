using log4net.Util;
using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public static class DataStorage
    {
        //For demo purpose, we store objects as strings in string file;
        private static string systemPath =  Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private const string paymentsFileName = "Payments.txt";

        private static string paymentsFullPath { get => Path.Combine(systemPath, paymentsFileName); }


        public static async Task<List<PaymentDetailsExtended>> WritePayments(params PaymentDetails[] payments)
        {
            Logs.Info("Attempting to write " + payments.Length + " PaymentDetails to storage");

            List<PaymentDetailsExtended> output = new List<PaymentDetailsExtended>();
            List<string> stringObjects = new List<string>();

            try
            {
                foreach (PaymentDetails item in payments)
                {
                    //Introduce ID field to the object.
                    PaymentDetailsExtended extended = new PaymentDetailsExtended(item)
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    //Add object 
                    stringObjects.Add(JsonSerializer.Serialize(extended));
                    output.Add(extended);
                }

                await File.AppendAllLinesAsync(paymentsFullPath, stringObjects);
                Logs.Info("Write success");
                return output;
            }
            catch (Exception e)
            {
                Logs.Error("Error writing data", e);
                return null;
            }
        }
        public static async Task<PaymentDetailsExtended> GetData(string id)
        {
            Logs.Info("Attempting to read payment for id: " + id);
            if (File.Exists(paymentsFullPath) == false) return null;

            List<string> stringObjects = new List<string>();
            try
            {
                foreach (string item in await File.ReadAllLinesAsync(paymentsFullPath))
                {
                    PaymentDetailsExtended payment = JsonSerializer.Deserialize<PaymentDetailsExtended>(item);
                    if (payment != null && payment.Id == id)
                    {
                        Logs.Info("Payment found for id: " + id);
                        return payment;
                    }
                }
                return null;

            }
            catch (Exception e)
            {
                Logs.Error("Error reading data", e);
                return null;

            }
        }
    }
}
