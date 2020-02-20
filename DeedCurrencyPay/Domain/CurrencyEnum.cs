using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace DeedCurrencyPay.Domain
{
    public enum CurrencyEnum
    {
        RUB,

        USD,

        EUR,

        IDR
    }
}

/*

public class Currency
    {
        private readonly KeyValuePair<CurrencyEnum, double> LeadCurrency;
        private readonly IDictionary<CurrencyEnum, double> _ReferenceRates;

        public Currency()
        {

            _ReferenceRates = new Dictionary<CurrencyEnum, double>();
            _ReferenceRates
                .Add(new KeyValuePair<CurrencyEnum, double>(CurrencyEnum.RUB, 68.6245));
            _ReferenceRates
                .Add(new KeyValuePair<CurrencyEnum, double>(CurrencyEnum.USD, 1.0800));
            _ReferenceRates
                .Add(new KeyValuePair<CurrencyEnum, double>(CurrencyEnum.EUR, 1));
            _ReferenceRates
                .Add(new KeyValuePair<CurrencyEnum, double>(CurrencyEnum.IDR, 14783.04));

            var LeadCurrencyKvP = new KeyValuePair<CurrencyEnum, double>(CurrencyEnum.EUR, _ReferenceRates[CurrencyEnum.EUR]);
        }

        public double ConvertCurrency(CurrencyEnum fromCurrency, CurrencyEnum toCurrency, double amount)
        {

        }

    }
    */


