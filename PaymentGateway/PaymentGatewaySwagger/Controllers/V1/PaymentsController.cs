using Microsoft.AspNetCore.Mvc;
using PaymentGatewaySwagger.Contracts;
using PaymentGatewaySwagger.Contracts.V1;
using PaymentGatewaySwagger.Contracts.V1.Requests;
using PaymentGatewaySwagger.Contracts.V1.Responses;
using PaymentGatewaySwagger.Data;
using PaymentGatewaySwagger.Domain;
using PaymentGatewaySwagger.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger.Controllers
{
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
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
        //[HttpGet("v1/payment")]
        //public IActionResult Get()
        //{
        //    return Ok(new { name = "dog" });
        //}
        
        [HttpGet(ApiRoutes.Payments.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(paymentService.GetPayments());
        }

        [HttpPost(ApiRoutes.Payments.Create)]
        public IActionResult Create([FromBody] CreatePaymentRequest payment)
        {
            var createdPayment = paymentService.CreatePayment(payment);
            if (createdPayment == null)
            {
                //Could also be server error/data source error.
                return UnprocessableEntity(); 
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Payments.Get.Replace("{id}", createdPayment.Id.ToString());

            return Created(locationUri, createdPayment);
        }

        [HttpGet(ApiRoutes.Payments.Get)]
        public IActionResult Get(Guid id)
        {
            var payment = paymentService.GetPaymentById(id);
            if(payment == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(payment);
            }
        }


        //[HttpGet("api/payment/{id}")]
        //public IActionResult GetPaymentInfo(string id)
        //{
        //    PaymentDetailsExtended payment = DataStorage.GetData(id).Result;
        //    if (payment == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(payment);
        //    }
        //}

        //[HttpPost]
        //public IActionResult ProcessPayment(PaymentDetails payment)
        //{
        //    var validResults = Helper.ValidateObject(payment);
        //    if (validResults.Count > 0)
        //    {
        //        string errorMessage = string.Empty;
        //        foreach (var dog in validResults)
        //        {
        //            errorMessage += dog.ErrorMessage + ";";
        //        }
        //        return BadRequest(errorMessage);
        //    }

        //    List<PaymentDetailsExtended> payments =  DataStorage.WritePayments(payment).Result;

        //    if (payments != null && payments.Count > 0)
        //    {
        //        return Created("ProcessPayment", payments[0]);
        //    }
        //    else
        //    {
        //        return Problem("Internal server error");
        //    }
        //}
    }
}
