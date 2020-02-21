using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UnitTests.TestHelpers;

namespace UnitTests
{
    [TestClass]
    public class UserEntityTests : TestBase
    {
        [TestInitialize]
        public void Setup()
        {
            TestBaseInitialize();
        }

        [TestMethod]
        public void Is_User_ValueEqual()
        {
            var dupeUsers = base.dupeUsers.ToList();
            Assert.IsFalse(UnitTestHelper<User>.Can_Add_Duplicate_To_HashSet(dupeUsers));
        }
    }
}