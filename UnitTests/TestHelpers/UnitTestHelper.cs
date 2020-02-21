using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitTests.TestHelpers
{
    internal class UnitTestHelper<T>
    {
        //todo move to TestBase
        internal static bool Can_Add_Duplicate_To_HashSet(IList<T> list)
        {
            HashSet<T> hashSet = new HashSet<T>();
            var array = list.ToArray();
            var hashsetAddResult = default(bool);
            for (int i = 0; i < array.Length; i++)
            {
                var item = array[i];
                hashSet.Add(item);
                if (i > 0)
                {
                    hashsetAddResult = hashSet.Add(item);
                }
                if (!hashsetAddResult)
                {
                    break;                   
                }
            }

            return hashsetAddResult;

            /*
            foreach (var item in list)
            {
                hashSet.Add(item);

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

    */

        }

        internal static bool Is_Immutable(Type type)
        {
            if (type.IsPrimitive) return true;
            if (type == typeof(string)) return true;
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var isShallowImmutable = fieldInfos.All(f => f.IsInitOnly);
            if (!isShallowImmutable) return false;
            var isDeepImmutable = fieldInfos.All(f => Is_Immutable(f.FieldType));
            return isDeepImmutable;
        }
    }
}