using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public class ExchangeRate
    {
        public string Currency { get; set; }
        public decimal Value { get; set; }
    }
}
