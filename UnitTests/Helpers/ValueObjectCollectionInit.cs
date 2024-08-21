using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Domain.Common;
using DeedCurrencyPay.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Helpers
{
    public class ValueObjectCollectionInit
    {
        public static IEnumerable<IValueObjectCollection<Money>> GetValueObjectCollectionList()
        {
            return GetBaseValueObjectCollectionList();
        }

        public static IEnumerable<IValueObjectCollection<Money>> GetValueObjectCollectionListWithDuplicates()
        {
            return GetBaseValueObjectCollectionList()
                .Concat(GetBaseValueObjectCollectionList());
        }

        private static IEnumerable<IValueObjectCollection<Money>> GetBaseValueObjectCollectionList()
        {
            return
                new List<IValueObjectCollection<Money>> {
                    MoneyListInit.GetMoneyList1(),
                    MoneyListInit.GetMoneyList2(),
            };
        }
    }
}