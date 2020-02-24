using DeedCurrencyPay.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Helpers
{
    public class ValueObjectCollectionInit
    {
        public static IEnumerable<ValueObjectCollection<Money>> GetValueObjectCollectionList()
        {
            return GetBaseValueObjectCollectionList();
        }

        public static IEnumerable<ValueObjectCollection<Money>> GetValueObjectCollectionListWithDuplicates()
        {
            return GetBaseValueObjectCollectionList()
                .Concat(GetBaseValueObjectCollectionList());
        }

        private static IEnumerable<ValueObjectCollection<Money>> GetBaseValueObjectCollectionList()
        {
            return new List<ValueObjectCollection<Money>> {
            new ValueObjectCollection<Money>
            {
                new Money(10000, Currency.RUB),
                new Money(500, Currency.EUR),
                new Money(750, Currency.USD),
                new Money(300000, Currency.IDR),
                new Money(300, Currency.EUR),
                new Money(10500, Currency.RUB),
                new Money(10000, Currency.USD),
                new Money(30500500, Currency.IDR)
            },
                new ValueObjectCollection<Money>
            {
                    new Money(10700, Currency.RUB),
                    new Money(580, Currency.EUR),
                    new Money(720, Currency.USD),
                    new Money(305050, Currency.IDR),
                    new Money(400, Currency.EUR),
                    new Money(11500, Currency.RUB),
                    new Money(10750, Currency.USD),
                    new Money(30700300, Currency.IDR)
            }
            };
        }
    }
}