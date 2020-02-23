using DeedCurrencyPay.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Helpers
{
    public static class MoneyInit
    {
        public static MoneyList GetMoneyList()
        {
            return GetBaseMoneyList();
        }

        public static MoneyList GetMoneyListWithDuplicates()
        {
            var list1 = new List<string>();
            var list2 = new List<string>();
            list1.AddRange(list2);
            

            var baseMoneyList = GetBaseMoneyList();
            MoneyList dupeMoneyList = baseMoneyList.AddRange(GetBaseMoneyList());            

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

        private static MoneyList GetBaseMoneyList()
        {
            var moneyList = new MoneyList();
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