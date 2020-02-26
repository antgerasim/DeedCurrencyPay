
using DeedCurrencyPay.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Domain
{
    public class Account : Entity<Account>, IAggregateRoot
    {
        public Account(long id, Money balance, string userName, IEnumerable<Currency> currencies)
        {
            base.Id = id;
            Balance = balance;
            UserName = userName;
            Currencies = currencies;
        }

        public Money Balance { get; private set; }
        public IEnumerable<Currency> Currencies { get; private set; }
        public string UserName { get; private set; } //для усиления идентичности (Equals, GetHashCode) дублируем имя из User



        public Account ConvertToCurrency(Currency targetCurrency, ConversionAmount conversionResult)
        {

            if (targetCurrency == Balance.SelectedCurrency)
            {
                throw new ArgumentException("Невозможно конвертировать в одинаковую валюту ");
            }

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
            return $"{Id} {UserName} {Balance}";
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