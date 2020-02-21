namespace DeedCurrencyPay.Domain
{
    public sealed class ConversionAmount : ValueObject<ConversionAmount>
    {
        public Currency CurrencyFrom { get; }
        public Currency CurrencyTo { get; }
        public decimal ConvertedAmountValue { get; }

        public ConversionAmount(Currency currencyFrom, Currency currencyTo, decimal convertedAmount)
        {
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
            ConvertedAmountValue = convertedAmount;
        }
        /*
        protected override bool EqualsCore(ConversionAmount other)
        {
            return other != null && this.ConvertedAmountValue == other.ConvertedAmountValue && this.CurrencyFrom == other.CurrencyFrom && this.CurrencyTo == other.CurrencyTo;
        }

        protected override int GetHashCodeCore()
        {
            return this.ConvertedAmountValue.GetHashCode() ^ this.CurrencyFrom.GetHashCode() ^ this.CurrencyTo.GetHashCode();
        }
        */
        public override string ToString()
        {
            return $"{this.ConvertedAmountValue} {CurrencyTo}";
        }
    }
}