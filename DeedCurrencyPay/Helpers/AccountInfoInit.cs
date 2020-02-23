using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Helpers
{
    public static class AccountInfoInit
    {
        public static IEnumerable<AccountInfo> GetAccountInfoList()
        {
            return GetBaseAccountInfoList();
        }

        public static IEnumerable<AccountInfo> GetAccountInfosWithDuplicate()
        {
            var baseAccountInfoList = GetBaseAccountInfoList();
            var dupeAccountInfoList = baseAccountInfoList.Concat(GetBaseAccountInfoList());

            return dupeAccountInfoList;
        }

        private static IEnumerable<AccountInfo> GetBaseAccountInfoList()
        {
            var moneyArray = MoneyInit.GetMoneyList().ToArray();
            return new List<AccountInfo>() {
                new AccountInfo(moneyArray[0], new MoneyList(moneyArray.Take(4))),
                new AccountInfo(moneyArray[4], new MoneyList(moneyArray.Skip(4).Take(4)))};
        }
    }
}
