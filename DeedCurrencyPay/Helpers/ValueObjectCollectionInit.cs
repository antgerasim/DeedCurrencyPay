using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            new ValueObjectCollection<Money>{
                new Money(10000, Currency.RUB),
                new Money(500, Currency.EUR),
                new Money(750, Currency.USD),
                new Money(300000, Currency.IDR),
                new Money(300, Currency.EUR),
                new Money(10500, Currency.RUB),
                new Money(10000, Currency.USD),
                new Money(30500500, Currency.IDR)
            }
            };
        }


    }
}
