using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Helpers;
using System;
using System.Globalization;
using System.Xml;

namespace DeedCurrencyPay.Services
{
    public class CurrencyService
    {
        private readonly Currency LeadCurrency;
        private readonly string ApiRoute;

        public CurrencyService()
        {
            //todo appsettings
            LeadCurrency = Currency.EUR;
            ApiRoute = @"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        }

        public ConversionAmount GetConversionAmount(Currency fromCurr, Currency toCurr, decimal amount)
        {
            var cExchangeRateRslt = GetConversionExchangeRate(fromCurr, toCurr, amount);
            return new ConversionAmount (fromCurr, toCurr, cExchangeRateRslt.ExchangeRateValue);
        }

        public ConversionExchangeRate GetConversionExchangeRate(Currency fromCurr, Currency toCurr, decimal amount = 1)
        {
            if (fromCurr == LeadCurrency && toCurr == LeadCurrency)
            {
                throw new ArgumentException("Invalid Argument! Не могу получить курс обмена валюты с Евро на Евро");
            }
            try
            {
                var toRate = default(decimal);
                var fromRate = default(decimal);
                var rsltRate = default(decimal);

                if (fromCurr == LeadCurrency)
                {
                    toRate = GetCurrencyRateInEuro(toCurr.ToFriendlyString());
                    rsltRate = (amount * toRate);
                    return new ConversionExchangeRate(fromCurr, toCurr, rsltRate);
                }
                if (toCurr == LeadCurrency)
                {
                    fromRate = GetCurrencyRateInEuro(fromCurr.ToFriendlyString());
                    rsltRate = (amount / fromRate);
                    return new ConversionExchangeRate(fromCurr, toCurr, rsltRate);
                }
                toRate = GetCurrencyRateInEuro(toCurr.ToFriendlyString()) / 1;
                fromRate = GetCurrencyRateInEuro(fromCurr.ToFriendlyString()) / 1;
                rsltRate = (amount * toRate) / fromRate;
                return new ConversionExchangeRate(fromCurr, toCurr, rsltRate);
            }
            catch
            {
                return default(ConversionExchangeRate);
            }
        }

        private decimal GetCurrencyRateInEuro(string targetCurr)
        {
            if (targetCurr == "")
                throw new ArgumentException("Invalid Argument! Параметр currency не может быть пустым!");
            if (targetCurr == "eur")
                throw new ArgumentException("Invalid Argument! Не могу получить курс обмена валюты с Евро на Евро");

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
                        if (nodeCurr == targetCurr)
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