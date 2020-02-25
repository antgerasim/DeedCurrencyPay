using DeedCurrencyPay.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Xml;

namespace DeedCurrencyPay.API.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly string ApiRoute;
        private readonly Currency LeadCurrency;
        public CurrencyService(IConfiguration config)
        {
            LeadCurrency = Currency.Parse(config["LeadCurrency"]);
            ApiRoute = @$"{config["ApiRoute"]}";
        }

        public ConversionExchangeRate GetConversionExchangeRate(Currency fromCurr, Currency toCurr)
        {
            var cExchangeRateRslt = GetConversionAmount(fromCurr, toCurr, 1);

            return new ConversionExchangeRate(fromCurr, toCurr, cExchangeRateRslt.ConvertedAmountValue);
        }

        public ConversionAmount GetConversionAmount(Currency fromCurr, Currency toCurr, decimal amount)
        {
            if (fromCurr == LeadCurrency && toCurr == LeadCurrency)
            {
                throw new ArgumentException("Не могу получить курс обмена валюты с Евро на Евро");
            }
            try
            {
                var toRate = default(decimal);
                var fromRate = default(decimal);
                var rsltRate = default(decimal);

                if (fromCurr == LeadCurrency)
                {
                    toRate = GetCurrencyRateInEuro(toCurr);
                    rsltRate = (amount * toRate);
                    return new ConversionAmount(fromCurr, toCurr, rsltRate);
                }
                if (toCurr == LeadCurrency)
                {
                    fromRate = GetCurrencyRateInEuro(fromCurr);
                    rsltRate = (amount / fromRate);
                    return new ConversionAmount(fromCurr, toCurr, rsltRate);
                }
                toRate = GetCurrencyRateInEuro(toCurr) / 1;
                fromRate = GetCurrencyRateInEuro(fromCurr) / 1;
                rsltRate = (amount * toRate) / fromRate;
                return new ConversionAmount(fromCurr, toCurr, rsltRate);
            }
            catch
            {
                return default(ConversionAmount);
            }
        }

        private decimal GetCurrencyRateInEuro(Currency targetCurr)
        {
            if (targetCurr == null)
                throw new ArgumentException("Параметр currency не может быть пустым!");
            if (targetCurr == Currency.EUR)
                throw new ArgumentException("Не могу получить курс обмена валюты с Евро на Евро");

            try
            {
                var doc = new XmlDocument();
                doc.Load(ApiRoute);

                XmlNodeList nodes = doc.SelectNodes("//*[@currency]");

                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        var nodeCurr = node.Attributes["currency"].Value;
                        if (nodeCurr == targetCurr.Name)
                        {
                            return Decimal.Parse(node.Attributes["rate"].Value, NumberStyles.Any, new CultureInfo("en-Us"));
                        }
                    }
                }
                return default(decimal);
            }
            catch
            {
                return default(decimal);
            }
        }
    }
}