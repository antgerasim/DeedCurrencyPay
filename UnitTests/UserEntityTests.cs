using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.TestHelpers;

namespace UnitTests
{
    [TestClass]
    public class UserEntityTests
    {
        [TestMethod]
        public void Is_Account_ValueEqual()
        {
            var x = new User(100001, "Igor", new Account(new Money(10000, Currency.RUB), 1000101, "Igor"),
                new List<Currency> { Currency.RUB, Currency.USD, Currency.EUR });

            var y = new User(100001, "Igor", new Account(new Money(10000, Currency.RUB), 1000101, "Igor"),
                new List<Currency> { Currency.RUB, Currency.USD, Currency.EUR });

            var z = new User(100002, "Yulia", new Account(new Money(300000, Currency.IDR), 1000102, "Yulia"),
                new List<Currency> { Currency.IDR });

            HashSet<User> hashSet = new HashSet<User>();
            UnitTestHelper<User>.ValueEqualityTest(hashSet, x, y, z);
        }
    }
}
