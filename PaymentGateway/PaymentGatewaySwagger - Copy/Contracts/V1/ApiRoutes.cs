using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewaySwagger.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Payments
        {
            public const string GetAll = Base + "/payments";
            public const string Get = Base + "/payments/{id}";
            public const string Create = Base + "/payment";
        }
    }
}
