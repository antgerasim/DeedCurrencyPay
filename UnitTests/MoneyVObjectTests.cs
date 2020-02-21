using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnitTests.TestHelpers;

namespace UnitTests
{
    [TestClass]
    public class MoneyVObjectTests
    {
        [TestMethod]
        public void Is_Money_ValueEqual()
        {
            var x = new Money(100, Currency.USD);
            var y = new Money(100, Currency.USD);
            var z = new Money(300, Currency.IDR);

            HashSet<Money> hashSet = new HashSet<Money>();
            UnitTestHelper<Money>.ValueEqualityTest(hashSet, x, y, z);
        }

        [TestMethod]
        public void Is_Money_Immutable()
        {
            //var x = new Money(100, Currency.USD);       
            //var z = new Money(300, Currency.IDR);


            UnitTestHelper<Money>.IsImmutable(typeof(Money));
        }

        #region Exception Test

        [TestMethod]
        public void When_ConvertFromValue_Is_Null_InvalidOperationException()
        {
            var money = new Money(100, Currency.USD);           

            Assert.ThrowsException<InvalidOperationException>(() => money.ConvertToCurrency(0, Currency.USD, 1.11m));
        }

        [TestMethod]
        public void When_ExgRate_Is_Less_Or_Equal_Zero_InvalidOperationException()
        {
            var money = new Money(100, Currency.USD);
            var fromValue = new Money(10000, Currency.RUB);
            var exgRateZero = default(decimal);
            var exgRateNegative = -1m;

            Assert.ThrowsException<InvalidOperationException>(() => money.ConvertToCurrency(100, Currency.USD, exgRateZero));
            Assert.ThrowsException<InvalidOperationException>(() => money.ConvertToCurrency(100, Currency.USD, exgRateNegative));

        }

        [TestMethod]
        public void OperatorGreaterThen_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA > moneyB);
        }
        [TestMethod]
        public void OperatorLessThen_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA < moneyB);
        }
        [TestMethod]
        public void OperatorLessThenOrEquals_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA <= moneyB);
        }

        [TestMethod]
        public void OperatorGreaterThenOrEquals_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA >= moneyB);
        }

        [TestMethod]
        public void OperatorAddition_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA + moneyB);
        }

        [TestMethod]
        public void OperatorSubstracton_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA - moneyB);
        }

        [TestMethod]
        public void OperatorDivision_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA / moneyB);
        }

        [TestMethod]
        public void OperatorMultiplication_When_Money_Currency_Not_Same_InvalidOperationException()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.EUR);

            Assert.ThrowsException<InvalidOperationException>(() => moneyA * moneyB);
        }
        #endregion

        #region Logic - Boolean operators

        [TestMethod]
        public void ConvertToCurrency_WithValidAmount()
        {
            
            var fromValue = new Money(10000, Currency.RUB);
            var exgRate = 0.0159m;
            var expectedValid = new Money(159, Currency.USD); //make Assert.NotEqual other currency and resultValue
            var expectedInValidCurr = new Money(159, Currency.IDR);
            var expectedInValidAmount = new Money(250, Currency.USD);

            var actual = fromValue.ConvertToCurrency(fromValue.Amount,Currency.USD, exgRate);

            Assert.AreEqual(expectedValid, actual);
            Assert.AreNotEqual(expectedInValidCurr, actual);
            Assert.AreNotEqual(expectedInValidAmount, actual);
        }

        [TestMethod]
        public void Operator_Equality_Test()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(100, Currency.USD);
            var moneyC = new Money(150, Currency.USD);
            Assert.IsTrue(moneyA == moneyB);
            Assert.IsFalse(moneyA == moneyC);
        }

        [TestMethod]
        public void Operator_NonEquality_Test()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(100, Currency.USD);
            var moneyC = new Money(150, Currency.USD);

            Assert.IsTrue(moneyA != moneyC);
            Assert.IsFalse(moneyA != moneyB);            
        }

        [TestMethod]
        public void Operator_GreaterThen_Test()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(100, Currency.USD);
            var moneyC = new Money(150, Currency.USD);

            Assert.IsTrue(moneyC > moneyA);          
        }

        [TestMethod]
        public void Operator_LessThen_Test()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(100, Currency.USD);
            var moneyC = new Money(150, Currency.USD);

            Assert.IsTrue(moneyA < moneyC);                 
        }

        [TestMethod]
        public void Operator_LessOrEqualThen_Test()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(100, Currency.USD);
            var moneyC = new Money(150, Currency.USD);
          
            Assert.IsTrue(moneyA <= moneyC);
            Assert.IsTrue(moneyA == moneyB);
        }

        [TestMethod]
        public void Operator_GreaterOrEqualThen_Test()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(100, Currency.USD);
            var moneyC = new Money(150, Currency.USD);

            Assert.IsTrue(moneyC >= moneyA);
            Assert.IsTrue(moneyA == moneyB);
        }
        #endregion

        #region Logic Arithmetic  operators
        [TestMethod]
        public void OperatorAddition_Logic()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.USD);
            var moneyNeg = new Money(-300, Currency.USD);

            var expectedPos = new Money(300, Currency.USD);
            var actualPos = moneyA + moneyB;
            var expectedNeg = new Money(-200, Currency.USD);
            var actualNeg = moneyA + moneyNeg;

            Assert.AreEqual(expectedPos, actualPos);
            Assert.AreEqual(expectedNeg, actualNeg);
        }

        [TestMethod]
        public void OperatorSubstraction_Logic()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.USD);
            var moneyNeg1 = new Money(-500, Currency.USD);
            var moneyNeg2 = new Money(-300, Currency.USD);

            var expectedPos = new Money(100, Currency.USD);            
            var actualPos = moneyB - moneyA;

            var expectedNeg1 = new Money(-100, Currency.USD);
            var actualNeg1 = moneyA - moneyB;

            var expectedNeg2 = new Money(-200, Currency.USD);
            var actualNeg2 = moneyNeg1 - moneyNeg2;

            Assert.AreEqual(expectedPos, actualPos);
            Assert.AreEqual(expectedNeg1, actualNeg1);
            Assert.AreEqual(expectedNeg2, actualNeg2);
        }

        [TestMethod]
        public void OperatorMultiplication_Logic()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.USD);
            var moneyNeg = new Money(-100, Currency.USD);

            var expectedPos = new Money(20000, Currency.USD);
            var actualPos = moneyB * moneyA;

            var expectedNeg = new Money(-20000, Currency.USD);
            var actualNeg = moneyB * moneyNeg;

            Assert.AreEqual(expectedPos, actualPos);
            Assert.AreEqual(expectedNeg, actualNeg);
        }

        [TestMethod]
        public void OperatorDivision_Logic()
        {
            var moneyA = new Money(100, Currency.USD);
            var moneyB = new Money(200, Currency.USD);
            var moneyNeg = new Money(-100, Currency.USD);

            var expectedPos = new Money(2, Currency.USD);
            var actualPos = moneyB / moneyA;

            var expectedNeg = new Money(-2, Currency.USD);
            var actualNeg = moneyB / moneyNeg;

            Assert.AreEqual(expectedPos, actualPos);
            Assert.AreEqual(expectedNeg, actualNeg);
        }


        #endregion
    }
}
