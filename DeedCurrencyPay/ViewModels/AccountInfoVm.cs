﻿using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.ViewModels
{
    public class ResponseVm
    {
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
        public String Message { get; set; }
    }
}
