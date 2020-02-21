using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public sealed class ConversionExchangeRate : ValueObject<ConversionExchangeRate>
    {
        public Currency CurrencyFrom { get; }
        public Currency CurrencyTo { get; }
        public decimal ExchangeRateValue { get; }

        public ConversionExchangeRate(Currency currencyFrom, Currency currencyTo, decimal exchangeRate) 
        {
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
            ExchangeRateValue = exchangeRate;
        }
/*
        protected override bool EqualsCore(ConversionExchangeRate other)
        {
            return other != null && this.ExchangeRateValue == other.ExchangeRateValue && this.CurrencyFrom == other.CurrencyFrom && this.CurrencyTo == other.CurrencyTo;
        }

        protected override int GetHashCodeCore()
        {
            return this.ExchangeRateValue.GetHashCode() ^ this.CurrencyFrom.GetHashCode() ^ this.CurrencyTo.GetHashCode();
        }
        */
        public override string ToString()
        {
            return $"{this.ExchangeRateValue} {CurrencyTo}";
        }
    }
}
