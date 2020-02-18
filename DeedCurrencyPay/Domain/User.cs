using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public class User
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        // public IList<Account> Accounts { get; set; }

        public IList<Currency> Currencies { get; set; }
    }
}
