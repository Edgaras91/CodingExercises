using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger
{
    public enum PaymentStatus { Pending, Declined, Processed }
    public static class Extensions
    {
        public static string MaskString(this string source)
        {
            if (string.IsNullOrEmpty(source) || source.Length <= 4)
                return source;

            return source.Substring(source.Length - 4).PadLeft(source.Length, '*');
        }
    }
}
