using Microsoft.CodeAnalysis.CSharp.Syntax;
using PaymentGatewaySwagger.Contracts.V1.Requests;
using PaymentGatewaySwagger.Contracts.V1.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int CCV { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentStatus Status { get; set; }

        public Payment() { }

        public Payment(CreatePaymentRequest request)
        {
            Amount = request.Amount;
            CardNumber = request.CardNumber;
            CCV = request.CCV;
            Currency = request.Currency;
            ExpiryMonth = request.ExpiryMonth;
            ExpiryYear = request.ExpiryYear;
            Id = request.Id;
        }

        public PaymentResponse ConvertToPaymentResponse()
        {
            return new PaymentResponse()
            {
                Amount = Amount,
                CardNumber = CardNumber,
                CCV = CCV,
                Currency = Currency,
                ExpiryMonth = ExpiryMonth,
                ExpiryYear = ExpiryYear,
                Id = Id,
                CreatedAt = CreatedAt,
                Status = Status
            };
        }
    }

}
