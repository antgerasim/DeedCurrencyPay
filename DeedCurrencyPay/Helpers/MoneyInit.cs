using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Helpers
{
    public static class MoneyInit
    {
        public static IEnumerable<Money> GetMoneyList()
        {
            return GetBaseMoneyList();
        }

        public static IEnumerable<Money> GetMoneyListWithDuplicates()
        {
            var baseMoneyList = GetBaseMoneyList();
            var dupeMoneyList = baseMoneyList.Concat(GetBaseMoneyList());

            return dupeMoneyList;
        }

        private static IEnumerable<Money> GetBaseMoneyList()
        {
            var moneyList = new List<Money>();
            moneyList.Add(new Money(10000, Currency.RUB));
            moneyList.Add(new Money(300000, Currency.IDR));
            return moneyList;
        }
    }
}

/*
var x = new Money(100, Currency.USD);
var y = new Money(100, Currency.USD);
var z = new Money(300, Currency.IDR);
*/