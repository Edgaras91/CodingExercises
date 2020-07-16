using PaymentGatewaySwagger.Contracts.V1.Requests;
using PaymentGatewaySwagger.Contracts.V1.Responses;
using PaymentGatewaySwagger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger.Services
{
    public interface IPaymentService
    {
        List<PaymentResponse> GetPayments();

        PaymentResponse GetPaymentById(Guid postId);

        PaymentResponse CreatePayment(CreatePaymentRequest payment);
    }
}
