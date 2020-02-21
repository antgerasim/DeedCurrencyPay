using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Helpers
{
    public static class UsersInit
    {
        public static IEnumerable<User> GetAllUsers()
        {
            return GetBaseUsers();
        }

        public static IEnumerable<User> GetAllUsersWithDuplicate()
        {
            var baseUserList = GetBaseUsers();
            var dupeUserList = baseUserList.Concat(GetBaseUsers());

            return dupeUserList;
        }

        private static IEnumerable<User> GetBaseUsers()
        {
            var userList = new List<User>();
            userList.Add(new User(100001, "Igor", new Account(new Money(10000, Currency.RUB), 1000101, "Igor", new List<Currency> { Currency.RUB, Currency.USD, Currency.EUR })));
            userList.Add(new User(100002, "Yulia", new Account(new Money(300000, Currency.IDR), 1000102, "Yulia", new List<Currency> { Currency.IDR })));
            return userList;
        }
    }
}
