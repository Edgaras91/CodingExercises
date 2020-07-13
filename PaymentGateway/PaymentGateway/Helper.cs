using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public static class Helper
    {
        public static ICollection<ValidationResult> ValidateObject(object obj)
        {
            List<ValidationResult> result = new List<ValidationResult>();

            Validator.TryValidateObject(obj, new ValidationContext(obj), result, true);

            return result;

        }
    }
}
