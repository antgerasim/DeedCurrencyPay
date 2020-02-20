using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitTests
{
    [TestClass]
    public class MoneyObjectTests
    {

        [TestMethod]
        public void Is_Money_ValueEqual()
        {
            var x = new Money(100, CurrencyEnum.USD);
            var y = new Money(100, CurrencyEnum.USD);
            var z = new Money(300, CurrencyEnum.IDR);

            HashSet<Money> hashSet = new HashSet<Money>();
            UnitTestHelper<Money>.ValueEqualityTest(hashSet, x, y, z);
        }

        #region Exception Test

        [TestMethod]
        public void When_ConvertFromValue_Is_Null_InvalidOperationException()
        {
            var money = new Money(100, CurrencyEnum.USD);           

            Assert.ThrowsException<InvalidOperationException>(() => money.ConvertToCurrency(0, CurrencyEnum.USD, 1.11));
        }

        [TestMethod]
        public void When_ExgRate_Is_Less_Or_Equal_Zero_InvalidOperationException()
        {
            var money = new Money(100, CurrencyEnum.USD);
            var fromValue = new Money(10000, CurrencyEnum.RUB);
            var exgRateZero = default(double);
            var exgRateNegative = -1d;

            Assert.ThrowsException<InvalidOperationException>(() => money.ConvertToCurrency(100, CurrencyEnum.USD, exgRateZero));
            Assert.ThrowsException<InvalidOperationException>(() => money.ConvertToCurrency(100, CurrencyEnum.USD, exgRateNegative));

        }

        [TestMethod]
        public void OperatorGreaterThen_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA > moneyB);
        }
        [TestMethod]
        public void OperatorLessThen_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA < moneyB);
        }
        [TestMethod]
        public void OperatorLessThenOrEquals_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA <= moneyB);
        }

        [TestMethod]
        public void OperatorGreaterThenOrEquals_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA >= moneyB);
        }

        [TestMethod]
        public void OperatorAddition_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA + moneyB);
        }

        [TestMethod]
        public void OperatorSubstracton_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA - moneyB);
        }

        [TestMethod]
        public void OperatorDivision_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA / moneyB);
        }

        [TestMethod]
        public void OperatorMultiplication_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA * moneyB);
        }
        #endregion

        #region Logic - Boolean operators

        [TestMethod]
        public void ConvertToCurrency_WithValidAmount()
        {
            
            var fromValue = new Money(10000, CurrencyEnum.RUB);
            var exgRate = 0.0159d;
            var expectedValid = new Money(159, CurrencyEnum.USD); //make Assert.NotEqual other currency and resultValue
            var expectedInValidCurr = new Money(159, CurrencyEnum.IDR);
            var expectedInValidAmount = new Money(250, CurrencyEnum.USD);

            var actual = fromValue.ConvertToCurrency(fromValue.Amount,CurrencyEnum.USD, exgRate);

            Assert.AreEqual(expectedValid, actual);
            Assert.AreNotEqual(expectedInValidCurr, actual);
            Assert.AreNotEqual(expectedInValidAmount, actual);
        }

        [TestMethod]
        public void Operator_Equality_Test()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(100, CurrencyEnum.USD);
            var moneyC = new Money(150, CurrencyEnum.USD);
            Assert.IsTrue(moneyA == moneyB);
            Assert.IsFalse(moneyA == moneyC);
        }

        [TestMethod]
        public void Operator_NonEquality_Test()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(100, CurrencyEnum.USD);
            var moneyC = new Money(150, CurrencyEnum.USD);

            Assert.IsTrue(moneyA != moneyC);
            Assert.IsFalse(moneyA != moneyB);            
        }

        [TestMethod]
        public void Operator_GreaterThen_Test()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(100, CurrencyEnum.USD);
            var moneyC = new Money(150, CurrencyEnum.USD);

            Assert.IsTrue(moneyC > moneyA);          
        }

        [TestMethod]
        public void Operator_LessThen_Test()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(100, CurrencyEnum.USD);
            var moneyC = new Money(150, CurrencyEnum.USD);

            Assert.IsTrue(moneyA < moneyC);                 
        }

        [TestMethod]
        public void Operator_LessOrEqualThen_Test()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(100, CurrencyEnum.USD);
            var moneyC = new Money(150, CurrencyEnum.USD);
          
            Assert.IsTrue(moneyA <= moneyC);
            Assert.IsTrue(moneyA == moneyB);
        }

        [TestMethod]
        public void Operator_GreaterOrEqualThen_Test()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(100, CurrencyEnum.USD);
            var moneyC = new Money(150, CurrencyEnum.USD);

            Assert.IsTrue(moneyC >= moneyA);
            Assert.IsTrue(moneyA == moneyB);
        }
        #endregion

        #region Logic Arithmetic  operators
        [TestMethod]
        public void OperatorAddition_Logic()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.USD);
            var moneyNeg = new Money(-300, CurrencyEnum.USD);

            var expectedPos = new Money(300, CurrencyEnum.USD);
            var actualPos = moneyA + moneyB;
            var expectedNeg = new Money(-200, CurrencyEnum.USD);
            var actualNeg = moneyA + moneyNeg;

            Assert.AreEqual(expectedPos, actualPos);
            Assert.AreEqual(expectedNeg, actualNeg);
        }

        [TestMethod]
        public void OperatorSubstraction_Logic()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.USD);
            var moneyNeg1 = new Money(-500, CurrencyEnum.USD);
            var moneyNeg2 = new Money(-300, CurrencyEnum.USD);

            var expectedPos = new Money(100, CurrencyEnum.USD);            
            var actualPos = moneyB - moneyA;

            var expectedNeg1 = new Money(-100, CurrencyEnum.USD);
            var actualNeg1 = moneyA - moneyB;

            var expectedNeg2 = new Money(-200, CurrencyEnum.USD);
            var actualNeg2 = moneyNeg1 - moneyNeg2;

            Assert.AreEqual(expectedPos, actualPos);
            Assert.AreEqual(expectedNeg1, actualNeg1);
            Assert.AreEqual(expectedNeg2, actualNeg2);
        }

        [TestMethod]
        public void OperatorMultiplication_Logic()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.USD);
            var moneyNeg = new Money(-100, CurrencyEnum.USD);

            var expectedPos = new Money(20000, CurrencyEnum.USD);
            var actualPos = moneyB * moneyA;

            var expectedNeg = new Money(-20000, CurrencyEnum.USD);
            var actualNeg = moneyB * moneyNeg;

            Assert.AreEqual(expectedPos, actualPos);
            Assert.AreEqual(expectedNeg, actualNeg);
        }

        [TestMethod]
        public void OperatorDivision_Logic()
        {
            var moneyA = new Money(100, CurrencyEnum.USD);
            var moneyB = new Money(200, CurrencyEnum.USD);
            var moneyNeg = new Money(-100, CurrencyEnum.USD);

            var expectedPos = new Money(2, CurrencyEnum.USD);
            var actualPos = moneyB / moneyA;

            var expectedNeg = new Money(-2, CurrencyEnum.USD);
            var actualNeg = moneyB / moneyNeg;

            Assert.AreEqual(expectedPos, actualPos);
            Assert.AreEqual(expectedNeg, actualNeg);
        }


        #endregion
    }
}
