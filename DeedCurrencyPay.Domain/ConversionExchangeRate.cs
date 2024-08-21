using DeedCurrencyPay.Domain.Common;

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

        public override string ToString()
        {
            return $"{this.ExchangeRateValue} {CurrencyTo}";
        }
    }
}
