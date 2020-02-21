using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Helpers
{
    public static class EnumUtil
    {
        public static string ToFriendlyString(this Currency currency)
        {
            switch (currency)
            {
                case Currency.RUB:
                    return "RUB";
                case Currency.USD:
                    return "USD";
                case Currency.EUR:
                    return "EUR";
                case Currency.IDR:
                    return "IDR";
                default:
                    return "Такой валюты не существует";
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

    }
}
