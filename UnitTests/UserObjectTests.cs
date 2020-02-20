using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class UserObjectTests
    {
        [TestMethod]
        public void Is_Account_ValueEqual()
        {
            var x = new User
            {
                Id = 1,
                Name = "Igor",
                Account = new Account(new Money(10000, CurrencyEnum.RUB), 1000101, "Igor")
            };
            var y = new User
            {
                Id = 1,
                Name = "Igor",
                Account = new Account(new Money(10000, CurrencyEnum.RUB), 1000101, "Igor")
            };

            var z = new User
            {
                Id = 3,
                Name = "Petr",
                Account = new Account(new Money(2000, CurrencyEnum.IDR), 1000103, "Petr")
            };

            HashSet<User> hashSet = new HashSet<User>();
            UnitTestHelper<User>.ValueEqualityTest(hashSet, x, y, z);
        }
    }
}
