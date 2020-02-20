using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.ViewModels
{
    public class AccountInfoVm
    {
        public decimal Balance { get; set; }
        public CurrencyEnum Currency { get; set; }
        public String Message { get; set; }
    }
}
