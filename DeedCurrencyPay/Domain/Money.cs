using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public sealed class Money : IEquatable<Money>
    {
        public decimal Amount { get; }//todo to private set
        public CurrencyEnum SelectedCurrency { get; } //todo to private set

        public Money(decimal amount, CurrencyEnum currency)
        {
            this.Amount = amount;
            this.SelectedCurrency = currency;
        }

        public Money ConvertToCurrency(decimal fromValue, CurrencyEnum toCurrency, double exgRate)
        {
            if (fromValue <= 0 || exgRate <= 0)// Неверное значение баланса или курса обмена
                throw new InvalidOperationException("Wrong amount or exchange rate");

            return new Money(fromValue * (decimal)exgRate, toCurrency);
        }

        public static bool operator ==(Money a, Money b)
        {
            //ReferenceEquals избегаем безконечную рекурсию
            return (object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null)) ||
                    (!object.ReferenceEquals(a, null) && a.Equals(b));
        }
        public static bool operator !=(Money a, Money b)
        {
            return !(a == b);
        }

        public static bool operator >(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            return a.Amount > b.Amount;
        }

        public static bool operator <(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            if (a == b) return false;
            return !(a > b);
        }

        public static bool operator <=(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            if (a < b || a == b) return true;
            return false;
        }
        public static bool operator >=(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            if (a > b || a == b) return true;
            return false;
        }

        public static Money operator +(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            return new Money(a.Amount + b.Amount, a.SelectedCurrency);
        }

        public static Money operator -(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            return new Money(a.Amount - b.Amount, a.SelectedCurrency);
        }

        public static Money operator *(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            return new Money(a.Amount * b.Amount, a.SelectedCurrency);
        }

        public static Money operator /(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            return new Money(a.Amount / b.Amount, a.SelectedCurrency);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Money);
        }

        public bool Equals(Money other) //реализация IEquatable<Money>
        {
            return other != null && this.Amount == other.Amount && this.SelectedCurrency == other.SelectedCurrency;
        }

        public override int GetHashCode()
        {
            return this.Amount.GetHashCode() ^ this.SelectedCurrency.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Amount} {this.SelectedCurrency}";
        }

        private static void CurrencyExceptionCheck(Money a, Money b)
        {
            if (a.SelectedCurrency != b.SelectedCurrency)
            {
                throw new InvalidOperationException("Операция использует разные валюты!");
            }
        }
    }
}

