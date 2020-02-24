using DeedCurrencyPay.Domain;
using System.Collections.Generic;

namespace DeedCurrencyPay.Helpers
{
    public static class MoneyListInit
    {
        public static ValueObjectCollection<Money> GetMoneyList()
        {
            return GetBaseMoneyList();
        }

        public static ValueObjectCollection<Money> GetMoneyListWithDuplicates()
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

        private static ValueObjectCollection<Money> GetBaseMoneyList()
        {
            return new ValueObjectCollection<Money>
            {
                new Money(10000, Currency.RUB),
                new Money(500, Currency.EUR),
                new Money(750, Currency.USD),
                new Money(300000, Currency.IDR),
                new Money(300, Currency.EUR),
                new Money(10500, Currency.RUB),
                new Money(10000, Currency.USD),
                new Money(30500500, Currency.IDR)
            };
        }
    }
}