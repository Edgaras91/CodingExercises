using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        //[HttpGet]
        //public ActionResult<IEnumerable<PaymentDetailsMasked>> GetCards()
        //{
        //    return new List<PaymentDetailsMasked>() { new PaymentDetailsMasked() { CardNumber = "xxxx-xxxx-xxxx-" + new Random().Next(1000, 9999) } };
        //}

        //[HttpGet("{count}")]
        //public ActionResult<IEnumerable<PaymentDetailsMasked>> GetPaymentInfo(int count)
        //{
        //    var cards = new List<PaymentDetailsMasked>();

        //    for (int i = 0; i < count; i++)
        //    {
        //        //Generate new Masked Cards.
        //        DateTime expiryDate = DateTime.Today.AddDays(new Random().Next(1, 1000));

        //        var card = new PaymentDetailsMasked()
        //        {
        //            CardNumber = "xxxx-xxxx-xxxx-" + new Random().Next(1000, 9999),
        //            ExpiryMonth = expiryDate.Month,
        //            ExpiryYear = int.Parse(expiryDate.ToString("yy"))
        //        };
        //        cards.Add(card);
        //    }

        //    return cards;
        //}

        [HttpGet("api/payment/{id}")]
        public IActionResult GetPaymentInfo(string id)
        {
            PaymentDetailsExtended payment = DataStorage.GetData(id).Result;
            if (payment == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(payment);
            }
        }

        [HttpPost]
        public IActionResult ProcessPayment(PaymentDetails payment)
        {
            var validResults = Helper.ValidateObject(payment);
            if (validResults.Count > 0)
            {
                string errorMessage = string.Empty;
                foreach (var dog in validResults)
                {
                    errorMessage += dog.ErrorMessage + ";";
                }
                return BadRequest(errorMessage);
            }

            List<PaymentDetailsExtended> payments =  DataStorage.WritePayments(payment).Result;

            if (payments != null && payments.Count > 0)
            {
                return Created("ProcessPayment", payments[0]);
            }
            else
            {
                return Problem("Internal server error");
            }
        }
    }
}
