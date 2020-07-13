using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PaymentGateway.Models
{
    public class PaymentDetails
    {         
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public decimal Amount { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public string Currency { get; set; }

        //[MaxLength(999, ErrorMessage = "{0} is too long, can not be longer than {1} characters")]
        public int CCV { get; set; }

    }
    public class PaymentDetailsExtended : PaymentDetails
    {
        public string Id { get; set; }

        public PaymentDetailsExtended() { }
        public PaymentDetailsExtended(PaymentDetails payment)
        {
            //Copies all property values from payment to this (PaymentDetailsExtended)
            foreach (PropertyInfo property in payment.GetType().GetProperties())
            {
                this.GetType().GetProperty(property.Name)
                    .SetValue(this,property.GetValue(payment));
            } 
        }
    }
}
