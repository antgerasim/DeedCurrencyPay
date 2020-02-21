using DeedCurrencyPay.Domain;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<Money> GetOperatorTestMoney()
        {
            return new[]
            {
                new Money(100, Currency.USD),
                new Money(200, Currency.EUR)
            };
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