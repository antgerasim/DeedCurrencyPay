using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UserEntityTests : TestBase<User>
    {
        [TestInitialize]
        public void Setup()
        {
            TestInitializeBase();
        }

        [TestMethod]
        public void Is_User_ValueEqual()
        {
            var uniqueUsers = base.uniqueUsers.ToList();
            var dupeUsers = base.dupeUsers.ToList();

            Assert.IsTrue(Can_Add_To_HashSet(uniqueUsers));
            Assert.IsFalse(Can_Add_To_HashSet(dupeUsers));
        }
    }
}