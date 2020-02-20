using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    internal static class UnitTestHelper<T>
    {
        internal static void ValueEqualityTest(HashSet<T> hashSet, T equalA, T equalB, T nonEqualC)
        {
            hashSet.Add(equalA);

            var hashSetContainsResult = hashSet.Contains(equalB);
            var hashSetResult = hashSet.Add(equalB);

            Assert.IsTrue(hashSetContainsResult, "HashSet уже содержит запись с аналогичным по содержанию значением");
            Assert.IsFalse(hashSetResult, "Нельзя добавлять в hashSet значение дубликат");
            Assert.AreEqual(equalA, equalB);
            Assert.AreNotSame(equalA, equalB);
            Assert.AreNotEqual(equalA, nonEqualC);

            Assert.IsTrue(equalA.Equals(equalB));
            Assert.IsFalse(!equalA.Equals(equalB));

        }


    }
}