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
            return
                new List<ValueObjectCollection<Money>> {
                    MoneyListInit.GetMoneyList1(),
                    MoneyListInit.GetMoneyList2(),
            };
        }
    }
}