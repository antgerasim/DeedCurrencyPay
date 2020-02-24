﻿using DeedCurrencyPay.Domain;
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
            var moneyCollection = MoneyInit.GetMoneyCollection();
            return new List<AccountInfo>() {
               // new AccountInfo(moneyCollection[0], new ValueObjectCollection<Money>(moneyCollection.Take(4))),
                new AccountInfo(moneyCollection.ElementAt(0), new ValueObjectCollection<Money>(moneyCollection.Take(4).ToList())),

                //new AccountInfo(moneyCollection[4], new ValueObjectCollection<Money>(moneyCollection.Skip(4).Take(4)))};
                new AccountInfo(moneyCollection.ElementAt(4), new ValueObjectCollection<Money>(moneyCollection.Skip(4).Take(4).ToList()))
            };

        }
    }
}
