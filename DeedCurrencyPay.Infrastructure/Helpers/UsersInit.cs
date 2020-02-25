using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Infrastructure.Helpers
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
            return new List<User> {
            new User(100104, "Viktor", new Account(1000101, new Money(10000, Currency.RUB), "Viktor", new List<Currency> { Currency.RUB, Currency.USD, Currency.EUR })),
            new User(100105, "Elena", new Account(1000102, new Money(300000, Currency.IDR), "Elena", new List<Currency> { Currency.IDR }))
        };
        }
    }
}
