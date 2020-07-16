using PaymentGatewaySwagger.Contracts.V1.Requests;
using PaymentGatewaySwagger.Contracts.V1.Responses;
using PaymentGatewaySwagger.Data;
using PaymentGatewaySwagger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentResponse CreatePayment(CreatePaymentRequest payment)
        {


            //not running async
            var domainPayment = FileStorage.WritePayment(new Payment(payment)).Result;
            if (domainPayment == null)
                return null;

            return domainPayment.ConvertToPaymentResponse();
        }

        public PaymentResponse GetPaymentById(Guid postId)
        {
            //not running async
            var domainPayment = FileStorage.GetPayment(postId).Result;
            if (domainPayment == null) return null;

            return domainPayment.ConvertToPaymentResponse();
        }

        public List<PaymentResponse> GetPayments()
        {
            //not running async
            var domainPayments = FileStorage.GetPayments().Result;
            if (domainPayments == null) return null;

            List<PaymentResponse> list = new List<PaymentResponse>();

            foreach (var item in domainPayments)
            {
                list.Add(item.ConvertToPaymentResponse());
            }
            return list;
        }
    }
}
