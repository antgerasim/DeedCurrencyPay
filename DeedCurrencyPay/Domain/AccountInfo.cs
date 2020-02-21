using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public class AccountInfo
    {
        public Money Money { get; }

        public AccountInfo(Money money)
        {
            Money = money;
        }
        
    }
}
