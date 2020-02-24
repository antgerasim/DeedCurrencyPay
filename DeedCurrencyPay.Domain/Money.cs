using DeedCurrencyPay.Domain.Common;
using System;

namespace DeedCurrencyPay.Domain
{
    public sealed class Money : ValueObject<Money>
    {
        public Money(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.SelectedCurrency = currency;
        }

        public decimal Amount { get; }
        public Currency SelectedCurrency { get; }

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

        public static Money operator +(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            return new Money(a.Amount + b.Amount, a.SelectedCurrency);
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

        public static bool operator >(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b); 
            return a.Amount > b.Amount;
        }
        public static bool operator >=(Money a, Money b)
        {
            CurrencyExceptionCheck(a, b);
            if (a > b || a == b) return true;
            return false;
        }
        public override string ToString()
        {
            //return $"{this.Amount} {this.SelectedCurrency.ToString()}";
            return String.Format("{0:0.00}", this.Amount) + " " + this.SelectedCurrency.Name;

        }

        private static void CurrencyExceptionCheck(Money a, Money b)
        {
            if (a.SelectedCurrency != b.SelectedCurrency)
            {
                throw new ArgumentException("Операция использует разные валюты!");
            }
        }
    }
}