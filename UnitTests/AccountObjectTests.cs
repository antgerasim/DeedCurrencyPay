using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class AccountObjectTests
    {
        [TestMethod]
        public void Is_Account_ValueEqual()
        {
            var x = new Account(new Money(10000, CurrencyEnum.RUB), 1000101, "Igor");
            var y = new Account(new Money(10000, CurrencyEnum.RUB), 1000101, "Igor");
            var z = new Account(new Money(300, CurrencyEnum.RUB), 1000102, "Ernest");

            HashSet<Account> hashSet = new HashSet<Account>();
            UnitTestHelper<Account>.ValueEqualityTest(hashSet, x, y, z);
        }

        #region Exception Test

        [TestMethod]
        public void When_DepositAmount_Is_Less_Then_Zero_ArgumentOutOfRangeException()
        {
            var negativeMoney = new Money(-1, CurrencyEnum.RUB);
            var account = new Account(new Money(100, CurrencyEnum.RUB), 1000101, "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Deposit(negativeMoney));
        }

        [TestMethod]
        public void When_DepositAmount_Is_Equal_Zero_ArgumentOutOfRangeException()
        {
            var zeroMoney = new Money(0, CurrencyEnum.RUB);
            var account = new Account(new Money(100, CurrencyEnum.RUB), 1000101, "Igor");

            Assert.ThrowsException<InvalidOperationException>(() => account.Deposit(zeroMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Greater_Then_Balance_Amount_ArgumentOutOfRangeException()
        {
            var overLimitMoney = new Money(150, CurrencyEnum.RUB);
            var account = new Account(new Money(100, CurrencyEnum.RUB), 1000101, "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Withdraw(overLimitMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Less_Then_Zero_ArgumentOutOfRangeException()
        {
            var negativeMoney = new Money(-1, CurrencyEnum.RUB);
            var account = new Account(new Money(100, CurrencyEnum.RUB), 1000101, "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Withdraw(negativeMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Equal_Zero_ArgumentOutOfRangeException()
        {
            var negativeMoney = new Money(0, CurrencyEnum.RUB);
            var account = new Account(new Money(100, CurrencyEnum.RUB), 1000101, "Igor");

            Assert.ThrowsException<InvalidOperationException>(() => account.Withdraw(negativeMoney));
        }
        #endregion

        #region Logic Other
        [TestMethod]
        public void ConvertCurrency_WithValidAmount()
        {
            var account = new Account(new Money(10000, CurrencyEnum.RUB), 1000101, "Igor");
            //var exgRate = 0.0159d;
            var exgRate =  CurrencyConverter.GetExchangeRate(dollar, rouble, amount); ;


            var accountExpected = new Account(new Money(159, CurrencyEnum.USD), 1000101, "Igor");

            var accountActual = account.ConvertCurrency(CurrencyEnum.USD, exgRate);

            Assert.AreEqual(accountExpected, accountActual);
        }

        [TestMethod]
        public void GetAccountInfo_Test()
        {
            var currencyExgRateDict = new Dictionary<CurrencyEnum, double>();
            //currencyExgRateDict.Add(new KeyValuePair());

            var exgRate = 0.0159d;
            var account = new Account(new Money(10000, CurrencyEnum.RUB), 1000101, "Igor");
            var accountExpected = new Account(new Money(159, CurrencyEnum.USD), 1000101, "Igor");

            var accountActual = account.ConvertCurrency(CurrencyEnum.USD, exgRate);

            Assert.AreEqual(accountExpected, accountActual);

            //weiter mit hier
        }
        #endregion
    }
}
