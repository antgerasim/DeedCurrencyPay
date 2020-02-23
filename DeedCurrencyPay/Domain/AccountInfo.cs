using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeedCurrencyPay.Domain
{
    public class AccountInfo : ValueObject<AccountInfo>
    //public sealed class AccountInfo : ValueObjectMsd
    {
        public AccountInfo(Money balance, ValueObjectList otherCurrencies)
        {
            Balance = balance;            
            OtherCurrencies = otherCurrencies;
        }

        public Money Balance { get; }
        public ValueObjectList OtherCurrencies { get; }

        public override string ToString()
        {
            var sb = new StringBuilder($"Основной баланс кошелька: {this.Balance.ToString()}.");
            sb.Append($" Баланс кошелька в других валютах:");
            var array = OtherCurrencies.ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                var money = array[i];
                if (money.SelectedCurrency == Balance.SelectedCurrency)//не ковертируем в одинаковые валюты - перенести проверку в аккаунт
                {
                    continue;
                }

                if (i != array.Length - 1)
                {
                    sb.Append($" {money.ToString()},");
                }
                else
                {
                    sb.Append($" {money.ToString()}.");
                }
            }
            return sb.ToString();
        }

        private IList<Money> DuplicateCurrencyPolicy(Money balance, IEnumerable<Money> otherCurrencies)
        {
            return otherCurrencies.Where(curr => curr.SelectedCurrency != balance.SelectedCurrency).ToList();
        }
    }
}