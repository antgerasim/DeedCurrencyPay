using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitTests.TestHelpers;

namespace UnitTests
{
    [TestClass]
    public class AccountEntityTests : TestBase
    {
        [TestInitialize]
        public void Setup()
        {
            TestBaseInitialize();
        }

        [TestMethod]
        public void Is_Account_ValueEqual()
        {
            var dupeAccounts = base.dupeAccounts.ToList();            
            Assert.IsFalse(UnitTestHelper<Account>.Can_Add_Duplicate_To_HashSet(dupeAccounts));
        }

        #region Exception Test

        [TestMethod]
        public void When_DepositAmount_Is_Less_Then_Zero_ArgumentOutOfRangeException()
        {
            var negativeMoney = new Money(-1, Currency.RUB);            
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => igorAccount.Deposit(negativeMoney));
        }

        [TestMethod]
        public void When_DepositAmount_Is_Equal_Zero_ArgumentOutOfRangeException()
        {
            var zeroMoney = new Money(0, Currency.RUB);
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");

            Assert.ThrowsException<InvalidOperationException>(() => igorAccount.Deposit(zeroMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Greater_Then_Balance_Amount_ArgumentOutOfRangeException()
        {
            var overLimitMoney = new Money(150, Currency.RUB);
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => igorAccount.Withdraw(overLimitMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Less_Then_Zero_ArgumentOutOfRangeException()
        {
            var negativeMoney = new Money(-1, Currency.RUB);
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => igorAccount.Withdraw(negativeMoney));
        }

        [TestMethod]
        public void When_WithdrawAmount_Is_Equal_Zero_ArgumentOutOfRangeException()
        {
            var zeroMoney = new Money(0, Currency.RUB);
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");

            Assert.ThrowsException<InvalidOperationException>(() => igorAccount.Withdraw(zeroMoney));
        }

        [TestMethod]
        public void When_ConvertCurrency_WithInvalidEqualFromAndToCurrency_ArgumentException()
        {
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");
            var invalidCurrency = Currency.RUB;

            Assert.ThrowsException<ArgumentException>(() => igorAccount.ConvertToCurrency(invalidCurrency, base.currencyService));
        }
        #endregion

        #region Logic 
        [TestMethod]
        public void ConvertCurrency_WithValidCurrency()
        {
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");

            var accountConvertedrubToUsdRslt = igorAccount.ConvertToCurrency(Currency.USD, base.currencyService);

            Assert.IsNotNull(accountConvertedrubToUsdRslt);
            Assert.IsInstanceOfType(accountConvertedrubToUsdRslt, typeof(Account));
            Assert.IsTrue(accountConvertedrubToUsdRslt.Balance.Amount != 0);
            Assert.IsTrue(accountConvertedrubToUsdRslt.Balance.SelectedCurrency == Currency.USD);
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
