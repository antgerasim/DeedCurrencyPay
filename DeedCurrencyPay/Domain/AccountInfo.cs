using System;
using System.Collections.Generic;
using System.Text;

namespace DeedCurrencyPay.Domain
{
    public sealed class AccountInfo//valueobject mode
    {
        private readonly Money _Balance;
        private readonly IEnumerable<Money> _MoneyListOtherCurrencies;

        public AccountInfo(Money balance, IEnumerable<Money> moneyList)
        {
            this._Balance = balance;

            this._MoneyListOtherCurrencies = moneyList;
        }

        public override string ToString()
        { 
            var sb = new StringBuilder($"Основной баланс кошелька: {this._Balance.ToString() }.@");
        
            sb.Append($"Баланс кошелька в других валютах:@");

            foreach (var money in _MoneyListOtherCurrencies)
            {
                if (money.SelectedCurrency == _Balance.SelectedCurrency)//не ковертируем в одинаковые валюты - перенести проверку в аккаунт
                {
                    continue;
                }
                sb.Append($"{money.ToString()}@");
            }

            return sb.ToString().Replace("@", Environment.NewLine);
        }
    }
}