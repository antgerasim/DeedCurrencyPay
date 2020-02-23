using DeedCurrencyPay.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Helpers
{
    public static class MoneyInit
    {
        public static ValueObjectList GetMoneyList()
        {
            return GetBaseMoneyList();
        }

        public static ValueObjectList GetMoneyListWithDuplicates()
        {
            var baseMoneyList = GetBaseMoneyList();
            var dupeMoneyList = baseMoneyList.AddRange(GetBaseMoneyList());
            return dupeMoneyList;
        }

        public static IEnumerable<Money> GetOperatorTestMoney()
        {
            return new[]
            {
                new Money(100, Currency.USD),
                new Money(200, Currency.EUR)
            };
        }

        private static ValueObjectList GetBaseMoneyList()
        {
            var moneyList = new ValueObjectList();
            moneyList.Add(new Money(10000, Currency.RUB));
            moneyList.Add(new Money(500, Currency.EUR));
            moneyList.Add(new Money(750, Currency.USD));
            moneyList.Add(new Money(300000, Currency.IDR));
            moneyList.Add(new Money(300, Currency.EUR));
            moneyList.Add(new Money(10500, Currency.RUB));      
            moneyList.Add(new Money(10000, Currency.USD));
            moneyList.Add(new Money(30500500, Currency.IDR));
            return moneyList;
        }
    }
}