using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class MoneyVObjectTests : TestBase<Money>
    {
        [TestInitialize]
        public void Setup()
        {
            TestInitializeBase();
        }

        [TestMethod]
        public void Is_Money_ValueEqual()
        {
            var uniqueMoneyList = base.uniqueMoneyColl.ToList();
            var dupeMoneyList = base.dupeMoneyColl.ToList();

           // Assert.IsFalse(Can_Add_To_HashSet_Test_Mess(new[] { 1,1,2,2,3,3}));

            Assert.IsTrue(Can_Add_To_HashSet(uniqueMoneyList));
            Assert.IsFalse(Can_Add_To_HashSet(dupeMoneyList));
        }

        [TestMethod]
        public void Is_Money_Immutable()
        {
            Assert.IsTrue(TestBase<Money>.Is_Immutable(typeof(Money)));
        }

        #region Exception Test

        [TestMethod]
        public void OperatorGreaterThen_When_Money_Currency_Not_Same_ArgumentException()
        {
            var moneyA = basicTestMoneyArray[0];
            var moneyB = basicTestMoneyArray[1];

            Assert.ThrowsException<ArgumentException>(() => moneyA > moneyB);
        }

        [TestMethod]
        public void OperatorLessThen_When_Money_Currency_Not_Same_ArgumentException()
        {
            var moneyA = basicTestMoneyArray[0];
            var moneyB = basicTestMoneyArray[1];

            Assert.ThrowsException<ArgumentException>(() => moneyA < moneyB);
        }

        [TestMethod]
        public void OperatorLessThenOrEquals_When_Money_Currency_Not_Same_ArgumentException()
        {
            var moneyA = basicTestMoneyArray[0];
            var moneyB = basicTestMoneyArray[1];

            Assert.ThrowsException<ArgumentException>(() => moneyA <= moneyB);
        }

        [TestMethod]
        public void OperatorGreaterThenOrEquals_When_Money_Currency_Not_Same_ArgumentException()
        {
            var moneyA = basicTestMoneyArray[0];
            var moneyB = basicTestMoneyArray[1];

            Assert.ThrowsException<ArgumentException>(() => moneyA >= moneyB);
        }

        [TestMethod]
        public void OperatorAddition_When_Money_Currency_Not_Same_ArgumentException()
        {
            var moneyA = basicTestMoneyArray[0];
            var moneyB = basicTestMoneyArray[1];

            Assert.ThrowsException<ArgumentException>(() => moneyA + moneyB);
        }

        [TestMethod]
        public void OperatorSubstracton_When_Money_Currency_Not_Same_ArgumentException()
        {
            var moneyA = basicTestMoneyArray[0];
            var moneyB = basicTestMoneyArray[1];

            Assert.ThrowsException<ArgumentException>(() => moneyA - moneyB);
        }

        [TestMethod]
        public void OperatorDivision_When_Money_Currency_Not_Same_ArgumentException()
        {
            var moneyA = basicTestMoneyArray[0];
            var moneyB = basicTestMoneyArray[1];

            Assert.ThrowsException<ArgumentException>(() => moneyA / moneyB);
        }

        [TestMethod]
        public void OperatorMultiplication_When_Money_Currency_Not_Same_ArgumentException()
        {
            var moneyA = basicTestMoneyArray[0];
            var moneyB = basicTestMoneyArray[1];

            Assert.ThrowsException<ArgumentException>(() => moneyA * moneyB);
        }

        #endregion Exception Test

        #region Logic - Boolean operators

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

        #endregion Logic - Boolean operators

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

        #endregion Logic Arithmetic  operators
    }
}