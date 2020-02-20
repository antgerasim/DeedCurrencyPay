using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CurrencyConverterTest
    {
        [TestMethod]
        public void Convert_Test()
        {
            string dollar = "usd";
            string rouble = "rub";
            int amount = 1;

            var usdToRubRslt = CurrencyConverter.GetExchangeRate(dollar, rouble, amount);
            var rubToUsdRslt = CurrencyConverter.GetExchangeRate(rouble, dollar, amount);
            Assert.IsTrue(usdToRubRslt != 0);
            Assert.IsTrue(rubToUsdRslt != 0);
            Assert.IsTrue(usdToRubRslt > rubToUsdRslt);
        }
    }
}