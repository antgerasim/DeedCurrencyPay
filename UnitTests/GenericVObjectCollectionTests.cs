using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
   public class GenericVObjectCollectionTests : TestBase<ValueObjectCollection<Money>>
    {
        [TestInitialize]
        public void Setup()
        {
            TestInitializeBase();
        }

        [TestMethod]
        public void Is_GenericVObjectCollection_ValueEqual()
        {
            var uniqueVOCollectionObj = base.uniqueVOCollections.ToList();
            var dupeVOCollectionObj = base.dupeVOCollections.ToList(); 

            Assert.IsTrue(Can_Add_To_HashSet(uniqueVOCollectionObj));
            Assert.IsFalse(Can_Add_To_HashSet(dupeVOCollectionObj));
        }

        [TestMethod]
        public void Is_GenericVObjectCollection_Immutable()
        {
            Assert.IsTrue(TestBase<ValueObjectCollection<Money>>.Is_Immutable(typeof(ValueObjectCollection<Money>)));
        }

    }
}
