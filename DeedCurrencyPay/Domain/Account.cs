using DeedCurrencyPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Domain
{
    public class Account : Entity<Account>
    {
        public Account(long id, Money balance,  string userName, IEnumerable<Currency> currencies)
        {
            base.Id = id;
            Balance = balance;            
            UserName = userName;
            Currencies = currencies;
        }

        public Money Balance { get; private set; }
        public IEnumerable<Currency> Currencies { get; private set; }
        public string UserName { get; private set; } //для усиления идентичности (Equals, GetHashCode) дублируем имя из User

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
            if (money.Amount < 0)
            {
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
            //var moneyList = new List<Money>();
            var moneyList = new ValueObjectList();

            foreach (var targetCurrency in Currencies)
            {
                if (targetCurrency == Balance.SelectedCurrency)
                {
                    continue;
                }

                //var account = this.ConvertToCurrency(targetCurrency, currencyService);
                var convResult = currencyService.GetConversionAmount(Balance.SelectedCurrency, targetCurrency, Balance.Amount);

                //moneyList.Add(new Money(account.Balance.Amount, account.Balance.SelectedCurrency));
                moneyList.Add(new Money(convResult.ConvertedAmountValue, convResult.CurrencyTo));

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
                throw new ArgumentException("Для снятия денежных средств укажите суммму выше нуля.");
            }
            Balance -= money;
        }

        public override string ToString()
        {
            return $"{base.Id} {UserName} {Balance}";
        }

        private bool SelectedCurrencyCollisionPolicy(IEnumerable<Currency> currencies)
        {
            if (currencies.Contains(Balance.SelectedCurrency))
            {
                return false;
            }
            return true;
        }

    }
}