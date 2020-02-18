using DeedCurrencyPay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{

    public class Account
    {
        public Money Balance { get; private set; }

        public Account(Money balance)
        {
            this.Balance = balance;
        }

        public void Deposit(Money amount)
        {
            Balance += amount;
        }

        public void Withdraw(Money amount)
        {
            Balance -= amount;
        }

        public AccountInfoVm ConvertCurrency(Money moneyFrom, Currency currTo, double exgRate)
        {
            var retVal = Money.ConvertToCurrency(moneyFrom, currTo, exgRate).Amount;
            return new AccountInfoVm;
        }

        public IEnumerable<AccountInfoVm> GetAccountInfo(IList<Currency> currencies, double exgRate)
        {
            var retVal = currencies.Select(currency => new AccountInfoVm
            {
                Balance = Money.ConvertToCurrency
            (this.Balance, currency, exgRate).Amount,
                Currency = currency
            });
            return retVal;
        }
    }
}
