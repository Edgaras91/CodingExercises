using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Models
{
    public class PaymentDetailsMasked
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public string CardNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public int ExpiryMonth { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public int ExpiryYear { get; set; }
    }
}
