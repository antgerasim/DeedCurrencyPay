using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitTests.TestHelpers
{
    internal class UnitTestHelper<T>
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

        internal static bool IsImmutable(Type type)
        {
            if (type.IsPrimitive) return true;
            if (type == typeof(string)) return true;
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var isShallowImmutable = fieldInfos.All(f => f.IsInitOnly);
            if (!isShallowImmutable) return false;
            var isDeepImmutable = fieldInfos.All(f => IsImmutable(f.FieldType));
            return isDeepImmutable;
        }
    }
}