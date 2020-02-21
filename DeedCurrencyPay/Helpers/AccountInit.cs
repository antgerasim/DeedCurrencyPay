using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Helpers
{
    public static class AccountInit
    {
        private static IEnumerable<Currency> defaultThreeAccountCurrencies = new List<Currency> { Currency.RUB, Currency.USD, Currency.EUR };
        private static IEnumerable<Currency> defaultOneAccountCurrency = new List<Currency> { Currency.IDR };
        
        public static IEnumerable<Account> GetAllAccounts()
        {
            return GetBaseAccounts();
        }

        public static IEnumerable<Account> GetAllAccountsWithDuplicate()
        {
            var baseAccountList = GetBaseAccounts();
            var dupeAccountList = baseAccountList.Concat(GetBaseAccounts());

            return dupeAccountList;
        }

        private static IEnumerable<Account> GetBaseAccounts()
        {
            var accountList = new List<Account>();
            accountList.Add(new Account(new Money(100, Currency.RUB), 1000101, "Igor", defaultThreeAccountCurrencies));
            accountList.Add(new Account(new Money(300, Currency.IDR), 1000102, "Yulia", defaultOneAccountCurrency));
            return accountList;
        }
    }
}
