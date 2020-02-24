using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
   public class GenericVObjectCollectionTests : TestBase<IValueObjectCollection<Money>> 
    {
        [TestInitialize]
        public void Setup()
        {
            TestInitializeBase();
        }

        [TestMethod]
        public void Is_GenericVObjectCollection_ValueEqual()
        {  
            Assert.IsTrue(Can_Add_To_HashSet(uniqueVOCollections));
            Assert.IsFalse(Can_Add_To_HashSet(dupeVOCollections));
        }

        [TestMethod]
        public void Is_GenericVObjectCollection_Immutable()
        {
            Assert.IsTrue(TestBase<ValueObjectCollection<Money>>.Is_Immutable(typeof(ValueObjectCollection<Money>)));
        }
    }
}
