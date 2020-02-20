using System;
using System.Globalization;
using System.Xml;

namespace DeedCurrencyPay.Domain
{
    public class CurrencyConverter
    {
        public static float GetExchangeRate(string from, string to, float amount = 1)
        {
            if (from == null || to == null)
                return 0;

            if (from.ToLower() == "eur" && to.ToLower() == "eur")
                return amount;

            try
            {
                if (from.ToLower() == "eur")
                {
                    float toRates = (float)GetCurrencyRateInEuro(to).Value;
                    return (amount * toRates);
                }
                if (to.ToLower() == "eur")
                {
                    float fromRates = (float)GetCurrencyRateInEuro(from).Value;
                    return (amount / fromRates);
                }
                float toRate = (float)GetCurrencyRateInEuro(to).Value / 1;
                float fromRate = (float)GetCurrencyRateInEuro(from).Value / 1;

                return (amount * toRate) / fromRate;
            }
            catch { return 0; }
        }

        private static ExchangeRate GetCurrencyRateInEuro(string targetCurr)
        {
            if (targetCurr.ToLower() == "")
                throw new ArgumentException("Invalid Argument! Параметр currency не может быть пустым!");
            if (targetCurr.ToLower() == "eur")
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
                        var nodeCurr = node.Attributes["currency"].Value;
                        if (nodeCurr.ToLower() == targetCurr.ToLower())
                        {
                            return new ExchangeRate()
                            {
                                Currency = node.Attributes["currency"].Value,
                                Value = Decimal.Parse(node.Attributes["rate"].Value, NumberStyles.Any, new CultureInfo("en-Us"))
                            };
                        }
                    }
                }
                return default(ExchangeRate);
            }
            catch
            {
                return default(ExchangeRate);
            }
        }
    }
}