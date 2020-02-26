using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class AccountInfoVObjectTests : TestBase<AccountInfo>
    {
        [TestInitialize]
        public void Setup()
        {
            TestInitializeBase();
        }

        [TestMethod]
        public void Is_AccountInfo_ValueEqual()
        {
            var uniqueAccountInfoList = base.uniqueAccountInfoColl.ToList();
            var dupeAccountInfoList = base.dupeAccountInfoColl.ToList();

            Assert.IsTrue(Can_Add_To_HashSet(uniqueAccountInfoList));
            Assert.IsFalse(Can_Add_To_HashSet(dupeAccountInfoList));
        }

        [TestMethod]
        public void Is_AccountInfo_Immutable()
        {
            Assert.IsTrue(TestBase<Money>.Is_Immutable(typeof(AccountInfo)));
        }
    }
}
