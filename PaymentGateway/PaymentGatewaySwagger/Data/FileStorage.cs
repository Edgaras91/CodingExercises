using PaymentGatewaySwagger.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger.Data
{
    public class FileStorage
    {
        //For demo purpose, we store objects as strings in string file;
        private static string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private const string paymentsFileName = "Payments.txt";

        public static string paymentsFullPath = Path.Combine(systemPath, paymentsFileName);

        public static async Task<Payment> WritePayment(Payment payment)
        {
            //Logs.Info("Attempting to write " + payments.Length + " PaymentDetails to storage");

            List<string> stringObjects = new List<string>();

            try
            {
                if (payment.Id == null)
                    payment.Id = Guid.NewGuid();

                payment.CreatedAt = DateTime.Now;
                payment.Status = PaymentStatus.Pending;

                //Add object 
                stringObjects.Add(JsonSerializer.Serialize(payment));


                await File.AppendAllLinesAsync(paymentsFullPath, stringObjects);
                //Logs.Info("Write success");
                return payment;
            }
            catch (Exception)
            {
                //Logs.Error("Error writing data", e);
                return null;
            }
        }

        public static async Task<Payment> GetPayment(Guid id)
        {
            //Logs.Info("Attempting to read payment for id: " + id);
            if (File.Exists(paymentsFullPath) == false) return null;
            Payment payment;

            try
            {
                foreach (string item in await File.ReadAllLinesAsync(paymentsFullPath))
                {
                    try
                    {
                        payment = JsonSerializer.Deserialize<Payment>(item);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    if (payment != null && payment.Id == id)
                    {
                        //Logs.Info("Payment found for id: " + id);
                        return payment;
                    }
                }
                return null;

            }
            catch (Exception)
            {
                //Logs.Error("Error reading data", e);
                return null;
            }
        }

        public static async Task<List<Payment>> GetPayments()
        {
            //Logs.Info("Attempting to read payment for id: " + id);
            if (File.Exists(paymentsFullPath) == false) return null;
            List<Payment> payments = new List<Payment>();

            try
            {
                foreach (string item in await File.ReadAllLinesAsync(paymentsFullPath))
                {
                    try
                    {
                        var payment = JsonSerializer.Deserialize<Payment>(item);
                        payments.Add(payment);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return payments;

            }
            catch (Exception)
            {
                //Logs.Error("Error reading data", e);
                return null;
            }
        }
    }
}
