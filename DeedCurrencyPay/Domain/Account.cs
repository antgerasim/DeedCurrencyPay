using DeedCurrencyPay.Services;
using System;
using System.Collections.Generic;

namespace DeedCurrencyPay.Domain
{
    public class Account : Entity<Account>
    {
        public Money _Balance { get; private set; }

        private readonly string _UserName;

        public Account(Money balance, long id, string userName)
        {
            _Balance = balance;
            base._Id = id;
            _UserName = userName;

        }

        public void Deposit(Money money)
        {
            if (money.Amount < 0)
            {
                throw new ArgumentOutOfRangeException("Пополнение невозможно. Значение не может быть ниже нуля.");
            }
            if (money.Amount == 0)
            {
                throw new InvalidOperationException("Для пополнения кошелька укажите суммму выше нуля.");
            }
            _Balance += money;
        }

        public void Withdraw(Money money)
        {
            if (money.Amount > _Balance.Amount)
            {
                throw new ArgumentOutOfRangeException("Снятие невозможно. Запрашиваемая сумма выше доступных средтв.");
            }
            if (money.Amount < 0)
            {
                throw new ArgumentOutOfRangeException("Снятие невозможно. Значение не может быть ниже нуля.");
            }
            if (money.Amount == 0)
            {
                throw new InvalidOperationException("Для снятия денежных средств укажите суммму выше нуля.");
            }
            _Balance -= money;
        }

        public Account ConvertCurrency(Currency currTo)
        {
            if (currTo == _Balance.SelectedCurrency)
            {
                throw new ArgumentException("Invalid Argument! Невозможно конвертировать в одинаковую валюту ");
            }
            var conversionResult = new CurrencyService().GetConversionAmount(_Balance.SelectedCurrency, currTo, _Balance.Amount);
            _Balance = new Money(conversionResult.ConvertedAmountValue, conversionResult.CurrencyTo);
            return this;
        }

        //3.d. Получить состояние своего кошелька (сумму денег в каждой из валют)
        public IEnumerable<AccountInfo> GetAccountInfo(IList<Currency> currencyList) //todo replace IEnumerable<AccountInfo> with IEnumerable<Money>?
        {
            var accountInfoList = new List<AccountInfo>();
            foreach (var curr in currencyList)
            {
                var account = ConvertCurrency(curr);
                accountInfoList.Add(new AccountInfo(new Money(account._Balance.Amount, account._Balance.SelectedCurrency))); //caution immutability value reference stuff
            }
            return accountInfoList;
        }

        public override string ToString()
        {
            return $"{base._Id} {_UserName} {_Balance}";
        }
    }
}