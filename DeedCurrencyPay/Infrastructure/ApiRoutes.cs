using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.API.Infrastructure
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Account
        {
            public const string AccountInfo = Base + "/account/{userId}";
            public const string Convert = Base + "/convert/{userId}";
            public const string Deposit = Base + "/deposit/{userId}/{amount}"; 
            public const string Withdraw = Base + "/withdraw/{userId}/{amount}";
        }

    }
}
