using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PaymentGateway.Controllers;
using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Tests.Controllers
{
    class PaymentsControllerTest
    {
        PaymentDetails validPayment;
        PaymentDetailsExtended paymentResponse;

        PaymentsController controller;

        [SetUp]
        public void Setup()
        {
            controller = new PaymentsController();

            if (validPayment == null)
                validPayment = GenerateValidPayment();
        }

        private PaymentDetails GenerateValidPayment()
        {
            DateTime futureDate = DateTime.Now.AddDays(30).Date;

            return new PaymentDetails()
            {
                Amount = 200m,
                CardNumber = "111122223333" + new Random().Next(1000,9999).ToString(),
                CCV = 111,
                Currency = "GBP",
                ExpiryMonth = futureDate.Month,
                ExpiryYear = int.Parse(futureDate.ToString("yy"))
            };
        }

        [Test]
        public void ProcessPayment()
        {
            var response = controller.ProcessPayment(validPayment);
            if (response is CreatedResult)
            {
                CreatedResult createdResult = response as CreatedResult;
                if (createdResult.Value is PaymentDetailsExtended)
                {
                    PaymentDetailsExtended payment = createdResult.Value as PaymentDetailsExtended;
                    if (!string.IsNullOrEmpty(payment.Id))
                    {
                        paymentResponse = payment;
                        Assert.Pass("Success with id: " + payment.Id);
                        return;
                    }
                }
            }
            Assert.Fail("Could not retrieve new payment's Id");
        }

        [Test]
        public void RetrievePayment()
        {
            var response = controller.GetPaymentInfo(paymentResponse.Id);

            //if (response != null && response.Value is PaymentDetailsExtended)
            //{
            //    Assert.IsTrue(response.Value.Id == paymentResponse.Id);
            //}

            Assert.Fail("Could not retrieve new payment's Id");
        }
    }
}
