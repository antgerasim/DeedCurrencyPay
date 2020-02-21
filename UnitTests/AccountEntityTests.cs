using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.TestHelpers;

namespace UnitTests
{
    [TestClass]
    public class AccountEntityTests
    {
        [TestMethod]
        public void Is_Account_ValueEqual()
        {
            var x = new Account(new Money(10000, Currency.RUB), 1000101, "Igor");
            var y = new Account(new Money(10000, Currency.RUB), 1000101, "Igor");
            var z = new Account(new Money(300, Currency.RUB), 1000102, "Ernest");

            HashSet<Account> hashSet = new HashSet<Account>();
            UnitTestHelper<Account>.ValueEqualityTest(hashSet, x, y, z);
        }

        #region Exception Test

        [TestMethod]
        public void When_DepositAmount_Is_Less_Then_Zero_ArgumentOutOfRangeException()
        {
            var negativeMoney = new Money(-1, Currency.RUB);
            var account = new Account(new Money(100, Currency.RUB), 1000101, "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Deposit(negativeMoney));
        }

        [TestMethod]
        public void When_DepositAmount_Is_Equal_Zero_ArgumentOutOfRangeException()
        {
            var zeroMoney = new Money(0, Currency.RUB);
            var account = new Account(new Money(100, Currency.RUB), 1000101, "Igor");

            Assert.ThrowsException<InvalidOperationException>(() => account.Deposit(zeroMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Greater_Then_Balance_Amount_ArgumentOutOfRangeException()
        {
            var overLimitMoney = new Money(150, Currency.RUB);
            var account = new Account(new Money(100, Currency.RUB), 1000101, "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Withdraw(overLimitMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Less_Then_Zero_ArgumentOutOfRangeException()
        {
            var negativeMoney = new Money(-1, Currency.RUB);
            var account = new Account(new Money(100, Currency.RUB), 1000101, "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Withdraw(negativeMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Equal_Zero_ArgumentOutOfRangeException()
        {
            var zeroMoney = new Money(0, Currency.RUB);
            var account = new Account(new Money(100, Currency.RUB), 1000101, "Igor");

            Assert.ThrowsException<InvalidOperationException>(() => account.Withdraw(zeroMoney));
        }

        [TestMethod]
        public void When_ConvertCurrency_WithInvalidEqualFromAndToCurrency_ArgumentException()
        {
            var account = new Account(new Money(10000, Currency.RUB), 1000101, "Igor");

            Assert.ThrowsException<ArgumentException>(() => account.ConvertCurrency(Currency.RUB));
        }
        #endregion

        #region Logic 
        [TestMethod]
        public void ConvertCurrency_WithValidCurrency()
        {
            var account = new Account(new Money(10000, Currency.RUB), 1000101, "Igor");

            var accountConvertedrubToUsdRslt = account.ConvertCurrency(Currency.USD);

            Assert.IsNotNull(accountConvertedrubToUsdRslt);
            Assert.IsInstanceOfType(accountConvertedrubToUsdRslt, typeof(Account));
            Assert.IsTrue(accountConvertedrubToUsdRslt._Balance.Amount != 0);
            Assert.IsTrue(accountConvertedrubToUsdRslt._Balance.SelectedCurrency == Currency.USD);
        }

        [TestMethod]
        public void GetAccountInfo_Test()
        {/*
            var currencyExgRateDict = new Dictionary<Currency, double>();
            
            var account = new Account(new Money(10000, Currency.RUB), 1000101, "Igor");
            var accountExpected = new Account(new Money(159, Currency.USD), 1000101, "Igor");

            var accountActual = account.ConvertCurrency(Currency.USD);

            Assert.AreEqual(accountExpected, accountActual);
            */
            //weiter mit hier
        }
        #endregion
    }
}
