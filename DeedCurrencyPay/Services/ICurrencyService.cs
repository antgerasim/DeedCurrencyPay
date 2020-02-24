using DeedCurrencyPay.Domain;

namespace DeedCurrencyPay.API.Services
{
    public interface ICurrencyService
    {
        ConversionAmount GetConversionAmount(Currency fromCurr, Currency toCurr, decimal amount);
        ConversionExchangeRate GetConversionExchangeRate(Currency fromCurr, Currency toCurr);
    }
}