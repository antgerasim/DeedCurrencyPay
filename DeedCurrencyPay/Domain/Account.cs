using DeedCurrencyPay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{

    public sealed class Account : IEquatable<Account>
    {
        //private readonly User _user; user HAS A account
        public Money Balance { get; private set; }
        private readonly int _Id;
        private readonly string _UserName;

        public Account(Money balance, int id, string userName)
        {
            this.Balance = balance;
            _Id = id;
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
                throw new InvalidOperationException("Для снятия денежных средств укажите суммму выше нуля.");
            }
            Balance -= money;
        }

        public Account ConvertCurrency(CurrencyEnum currTo, double exgRate)
        {
            var moneyResult = Balance.ConvertToCurrency(Balance.Amount, currTo, exgRate);
            Balance = moneyResult;
            return this;            
        }
        //3.d. Получить состояние своего кошелька (сумму денег в каждой из валют)
        public IEnumerable<Account> GetAccountInfo(IDictionary<CurrencyEnum, double> currencyExgRateDict)
        {
            var retVal = currencyExgRateDict.Select(kvp => new Account(this.Balance.ConvertToCurrency(this.Balance.Amount, kvp.Key, kvp.Value), _Id, _UserName));
            return retVal;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Account);
        }
        public bool Equals(Account other) //реализация IEquatable<Money>
        {
            return other != null && this._Id == other._Id && this._UserName == other._UserName;
        }

        public override int GetHashCode()
        {
            return this._Id.GetHashCode() ^ this._UserName.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this._UserName} {this._Id}";
        }
    }
}
