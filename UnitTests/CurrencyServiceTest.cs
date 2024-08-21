using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class CurrencyServiceTest : TestBase<Currency>
    {

        [TestInitialize]
        public void Setup()
        {
            TestInitializeBase();
        }

        #region Exception Test
        [TestMethod]
        public void When_FromCurrency_And_ToCurrency_Are_Both_LeadCurrency_ArgumentException()
        {
            var euroFrom = Currency.EUR;
            var euroTo = Currency.EUR;  

            Assert.ThrowsException<ArgumentException>(() => currencyService.GetConversionExchangeRate(euroFrom, euroTo));
        }
        #endregion

        #region Logic 
        [TestMethod]
        public void GetExchangeRate_Valid()
        {
            var dollar = Currency.USD;
            var rouble = Currency.RUB;

            var usdToRubRslt = currencyService.GetConversionExchangeRate(dollar, rouble);
            var rubToUsdRslt = currencyService.GetConversionExchangeRate(rouble, dollar);

            Assert.IsNotNull(usdToRubRslt);
            Assert.IsNotNull(rubToUsdRslt);
            Assert.IsInstanceOfType(usdToRubRslt, typeof(ConversionExchangeRate));
            Assert.IsInstanceOfType(rubToUsdRslt, typeof(ConversionExchangeRate));
            Assert.IsTrue(usdToRubRslt.ExchangeRateValue != 0);
            Assert.IsTrue(rubToUsdRslt.ExchangeRateValue != 0);
            Assert.IsTrue(usdToRubRslt.ExchangeRateValue > rubToUsdRslt.ExchangeRateValue);
        }
        #endregion

    }
}