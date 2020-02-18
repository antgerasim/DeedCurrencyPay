using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public class Money
    {
        public decimal Amount { get; set; }//todo to private set
        public Currency SelectedCurrency { get; set; } //todo to private set

        public Money(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.SelectedCurrency = currency;
        }


        public static Money ConvertToCurrency(Money sourceValue, Currency destinationCurrency, double exchangeRate)
        {
            if (sourceValue == null || exchangeRate <= 0)
                throw new InvalidCastException("Wrong amount or exchange rate");

            return new Money(sourceValue.Amount * (decimal)exchangeRate, destinationCurrency);
        }

        public static bool operator ==(Money firstValue, Money secondValue)
        {
            if ((object)firstValue == null || (object)secondValue == null)
                return false;

            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency) return false;
            return firstValue.Amount == secondValue.Amount; //Bug?
        }

        public static bool operator !=(Money firstValue, Money secondValue)
        {
            return !(firstValue == secondValue);
        }

        public static bool operator >(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
                throw new InvalidOperationException("Comprasion between different currencies is not allowed.");

            return firstValue.Amount > secondValue.Amount;
        }

        public static bool operator <(Money firstValue, Money secondValue)
        {
            if (firstValue == secondValue) return false;

            return !(firstValue > secondValue);
        }

        public static bool operator <=(Money firstValue, Money secondValue)
        {
            if (firstValue < secondValue || firstValue == secondValue) return true;

            return false;
        }
        public static bool operator >=(Money firstValue, Money secondValue)
        {
            if (firstValue > secondValue || firstValue == secondValue) return true;

            return false;
        }

        public static Money operator +(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount + secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator -(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount - secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator *(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount * secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator /(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount / secondValue.Amount, firstValue.SelectedCurrency);
        }
    }
}
