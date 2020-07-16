using PaymentGatewaySwagger.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger.Contracts.V1.Responses
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        private string cardNumber;
        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value.MaskString(); }
        }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int CCV { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
