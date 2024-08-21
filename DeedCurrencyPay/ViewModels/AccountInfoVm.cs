using DeedCurrencyPay.Domain;
using System;

namespace DeedCurrencyPay.API.ViewModels
{
    public class ResponseVm
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public String Message { get; set; }
    }
}
