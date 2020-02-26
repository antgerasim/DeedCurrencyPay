using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Domain.Common;
using System.Collections.Generic;

namespace DeedCurrencyPay.Infrastructure.Helpers
{
    public static class MoneyListInit
    {
        public static IValueObjectCollection<Money> GetMoneyList1()
        {
            return GetBaseMoneyList1();
        }

        public static IValueObjectCollection<Money> GetMoneyList2()
        {
            return GetBaseMoneyList2();
        }

        public static IValueObjectCollection<Money> GetMoneyListWithDuplicates()
        {

            return GetBaseMoneyList1().AddRange(GetBaseMoneyList1());
        }

        public static IEnumerable<Money> GetOperatorTestMoney()
        {
            return new List<Money>
            {
                new Money(100, Currency.USD),
                new Money(200, Currency.EUR)
            };
        }

        private static IValueObjectCollection<Money> GetBaseMoneyList1()
        {            
            return new ValueObjectCollection<Money>()
                .AddImmutable(new Money(10000, Currency.RUB))
                .AddImmutable(new Money(500, Currency.EUR))
                .AddImmutable(new Money(750, Currency.USD))
                .AddImmutable(new Money(300000, Currency.IDR))
                .AddImmutable(new Money(300, Currency.EUR))
                .AddImmutable(new Money(10500, Currency.RUB))
                .AddImmutable(new Money(10000, Currency.USD))
                .AddImmutable(new Money(30500500, Currency.IDR));
        }
        private static IValueObjectCollection<Money> GetBaseMoneyList2()
        {
            return new ValueObjectCollection<Money>()
                .AddImmutable(new Money(10700, Currency.RUB))
                .AddImmutable(new Money(580, Currency.EUR))
                .AddImmutable(new Money(720, Currency.USD))
                .AddImmutable(new Money(305050, Currency.IDR))
                .AddImmutable(new Money(400, Currency.EUR))
                .AddImmutable(new Money(11500, Currency.RUB))
                .AddImmutable(new Money(10750, Currency.USD))
                .AddImmutable(new Money(30700300, Currency.IDR));
        }
    }
}