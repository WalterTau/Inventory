using System;
using System.Collections.Generic;
using System.Text;

namespace CashCrusadersFranchising.API.Service.Domain.Helpers
{
    public static class RouterHelper
    {
        public const string Index = "";
        public const string Version = Index + "v1{version:apiVersion}/";

        public static class V1
        {
            public const string Swagger = "v1/swagger.json";
            public const string Api = Version;
            public const string ApiNumber = "1";

            public const string Product = Version + "products";
            public const string Suppliers = Version + "suppliers";
            public const string PurchaseOrders = Version + "{appId}/testRunResult-executions";

        }

    }
}
