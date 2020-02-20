using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public class ConversionResult
    {
        public CurrencyEnum CurrencyFrom { get; }
        public CurrencyEnum CurrencyTo { get; }
        public decimal ExchangeRate { get; }

        public ConversionResult(CurrencyEnum currencyFrom, CurrencyEnum currencyTo, decimal exchangeRate)
        {
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
            ExchangeRate = exchangeRate;
        }
    }
}
