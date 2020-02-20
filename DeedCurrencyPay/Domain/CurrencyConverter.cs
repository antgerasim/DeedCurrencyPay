using DeedCurrencyPay.Helpers;
using System;
using System.Globalization;
using System.Xml;

namespace DeedCurrencyPay.Domain
{
    public class CurrencyConverter
    {
        private const CurrencyEnum LeadCurrency = CurrencyEnum.EUR;

        public static ConversionResult GetExchangeRate(CurrencyEnum fromCurr, CurrencyEnum toCurr, decimal amount = 1)
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
                    return new ConversionResult(fromCurr, toCurr, rsltRate);
                }
                if (toCurr == LeadCurrency)
                {
                    fromRate = GetCurrencyRateInEuro(fromCurr.ToFriendlyString());
                    rsltRate = (amount / fromRate);
                    return new ConversionResult(fromCurr, toCurr, rsltRate);
                }
                toRate = GetCurrencyRateInEuro(toCurr.ToFriendlyString()) / 1;
                fromRate = GetCurrencyRateInEuro(fromCurr.ToFriendlyString()) / 1;
                rsltRate = (amount * toRate) / fromRate;
                return new ConversionResult(fromCurr, toCurr, rsltRate);
            }
            catch
            {
                return default(ConversionResult);
            }
        }

        private static decimal GetCurrencyRateInEuro(string targetCurr)
        {
            if (targetCurr == "")
                throw new ArgumentException("Invalid Argument! Параметр currency не может быть пустым!");
            if (targetCurr == "eur")
                throw new ArgumentException("Invalid Argument! Не могу получить курс обмена валюты с Евро на Евро");

            try
            {
                var doc = new XmlDocument();
                doc.Load(@"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");//todo appsettings

                XmlNodeList nodes = doc.SelectNodes("//*[@currency]");

                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        var nodeCurr = node.Attributes["currency"].Value.ToLower();
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