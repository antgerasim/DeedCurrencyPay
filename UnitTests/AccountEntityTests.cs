using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class AccountEntityTests : TestBase<Account>
    {
        [TestInitialize]
        public void Setup()
        {
            TestInitializeBase();
        }

        [TestMethod]
        public void Is_Account_ValueEqual()
        {
            var uniqueAccounts = base.uniqueAccounts.ToList();
            var dupeAccounts = base.dupeAccounts.ToList();

            Assert.IsTrue(Can_Add_To_HashSet(uniqueAccounts));
            Assert.IsFalse(Can_Add_To_HashSet(dupeAccounts));
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
        public void When_WithdrawAmount_Is_Equal_Zero_ArgumentException()
        {
            var zeroMoney = new Money(0, Currency.RUB);
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");

            Assert.ThrowsException<ArgumentException>(() => igorAccount.Withdraw(zeroMoney));
        }

        [TestMethod]
        public void When_ConvertCurrency_WithInvalidEqualFromAndToCurrency_ArgumentException()
        {
            var igorAccount = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");
            var invalidCurrency = Currency.RUB;

            Assert.ThrowsException<ArgumentException>(() => igorAccount.ConvertToCurrency(invalidCurrency, base.currencyService));
        }

        #endregion Exception Test

        #region Logic


        [TestMethod]
        public void ConvertCurrency_WithValidCurrency_Rub_100_To_Usd()
        {
            var targetCurrency = Currency.USD;
            var accountUnderTest = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");
            var convertedAccount = accountUnderTest.ConvertToCurrency(targetCurrency, base.currencyService);
            var roundedResult = Math.Round(convertedAccount.Balance.Amount, 2, MidpointRounding.AwayFromZero);

            Assert_Currency_Convertion(targetCurrency, convertedAccount);
            Assert.IsTrue(IsBetween(roundedResult, 1, 2));
        }

        [TestMethod]
        public void ConvertCurrency_WithValidCurrency_Rub_15000_To_Eur()
        {
            var targetCurrency = Currency.EUR;
            var accountUnderTest = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Petr");
            var convertedAccount = accountUnderTest.ConvertToCurrency(targetCurrency, base.currencyService);
            var roundedResult = Math.Round(convertedAccount.Balance.Amount, 2, MidpointRounding.AwayFromZero);

            Assert_Currency_Convertion(targetCurrency, convertedAccount);
            Assert.IsTrue(IsBetween(roundedResult, 200, 250));
        }


        [TestMethod]
        public void ConvertCurrency_WithValidCurrency_Idr_100_To_Rub()
        {
            var targetCurrency = Currency.RUB;
            var accountUnderTest = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Yulia");
            var convertedAccount = accountUnderTest.ConvertToCurrency(targetCurrency, base.currencyService);
            var roundedResult = Math.Round(convertedAccount.Balance.Amount, 2, MidpointRounding.AwayFromZero);

            Assert_Currency_Convertion(targetCurrency, convertedAccount);
            Assert.IsTrue(IsBetween(roundedResult, 1.10m, 1.70m));
        }

        [TestMethod]
        public void GetAccountInfo_100_Rub_Other_Currencies_Usd_Eur()
        {
            var accountUnderTest = uniqueAccounts.FirstOrDefault(acc => acc.UserName == "Igor");
            var accountBalance = new Money(accountUnderTest.Balance.Amount, accountUnderTest.Balance.SelectedCurrency);
            var accountInfo = accountUnderTest.GetAccountInfo(base.currencyService);

            Assert.AreEqual(accountInfo.Balance, accountBalance);
            Assert.IsFalse(accountInfo.OtherCurrencies.Contains(accountInfo.Balance));
            //weiter mit hier
        }

        #endregion Logic
    }
}