using DeedCurrencyPay.Services;
using System;
using System.Collections.Generic;

namespace DeedCurrencyPay.Domain
{
    public class Account : Entity<Account>
    {
        public Account(Money balance, long id, string userName, IEnumerable<Currency> currencies)
        {
            Balance = balance;
            base.Id = id;
            UserName = userName;
            Currencies = currencies;
        }

        public Money Balance { get; private set; }
        public IEnumerable<Currency> Currencies { get; private set; }
        //для усиления идентичности (Equals, GetHashCode) дублируем имя из User
        public string UserName { get; private set; }

        public Account ConvertToCurrency(Currency toCurrency, ICurrencyService currencyService)
        {
            if (toCurrency == Balance.SelectedCurrency)
            {
                throw new ArgumentException("Невозможно конвертировать в одинаковую валюту ");
            }
            var conversionResult = currencyService.GetConversionAmount(Balance.SelectedCurrency, toCurrency, Balance.Amount);
            Balance = new Money(conversionResult.ConvertedAmountValue, conversionResult.CurrencyTo);
            return this;
        }

        public void Deposit(Money money)
        {
            if (money.Amount < 0) {
                throw new ArgumentOutOfRangeException("Пополнение невозможно. Значение не может быть ниже нуля.");
            }
            if (money.Amount == 0)
            {
                throw new InvalidOperationException("Для пополнения кошелька укажите суммму выше нуля.");
            }
            Balance += money;
        }

        // 3.d. Получить состояние своего кошелька (сумму денег в каждой из валют).
        // Передумать механизм, если после конвертации меняется текущий баланс кошелька!
        public AccountInfo GetAccountInfo(ICurrencyService currencyService)
        {
            var moneyList = new List<Money>();
            foreach (var targetCurrency in Currencies)
            {
                if (targetCurrency == Balance.SelectedCurrency)
                {
                    continue;
                }

                var account = this.ConvertToCurrency(targetCurrency, currencyService);
                moneyList.Add(new Money(account.Balance.Amount, account.Balance.SelectedCurrency));
            }
            var retVal = new AccountInfo(this.Balance, moneyList);
            return retVal;
        }

        public void Withdraw(Money money)
        {
            if (money.Amount > Balance.Amount)
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
            Balance -= money;
        }

        public override string ToString()
        {
            return $"{base.Id} {UserName} {Balance}";
        }
    }
}