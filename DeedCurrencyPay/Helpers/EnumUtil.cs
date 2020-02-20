using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Helpers
{
    public static class EnumUtil
    {
        public static string ToFriendlyString(this CurrencyEnum currency)
        {
            switch (currency)
            {
                case CurrencyEnum.RUB:
                    return "RUB";
                case CurrencyEnum.USD:
                    return "USD";
                case CurrencyEnum.EUR:
                    return "EUR";
                case CurrencyEnum.IDR:
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
