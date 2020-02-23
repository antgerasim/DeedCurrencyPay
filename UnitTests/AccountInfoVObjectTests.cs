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
            //var uniqueAccountInfoList = base.uniqueAccountInfoColl.ToList();
            var dupeAccountInfoList = base.dupeAccountInfoColl.ToList();

           // Assert.IsTrue(Can_Add_To_HashSet(uniqueAccountInfoList));
            Assert.IsFalse(Can_Add_To_HashSet(dupeAccountInfoList));
        }

        [TestMethod]
        public void Is_AccountInfo_Immutable()
        {
            Assert.IsTrue(TestBase<Money>.Is_Immutable(typeof(AccountInfo)));
        }

    }
}

//https://www.google.com/search?newwindow=1&client=firefox-b-d&sxsrf=ALeKk008jJgLFbmWnKPCx7gTyJu1CL5zEA%3A1582330976267&ei=YHRQXs_wD-mjmwXr74roDw&q=valueobjects+gethshcode+collection+list&oq=valueobjects+gethshcode+collection+list&gs_l=psy-ab.3...158880.170284..170385...0.2..0.105.2959.37j2......0....1..gws-wiz.......0i71j35i39j0i273j0j0i67j0i20i263j0i203j0i10j0i10i67j0i10i203j0i13j0i22i30j0i8i13i30j0i8i13i10i30j33i160j35i304i39j33i21.EyNO25D2DRw&ved=0ahUKEwjPoIee8uPnAhXp0aYKHeu3Av0Q4dUDCAo&uact=5
//https://stackoverflow.com/questions/10567450/implement-gethashcode-for-objects-that-contain-collections
//https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
//https://github.com/aspnetboilerplate/aspnetboilerplate/pull/4307/files
//https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/test/Abp.Tests/Domain/Values/ValueObject_Simple_Tests.cs

// Collections that the value objects are composed of maintain unique ids that seems to convert in different hashcodes!
//Solution: 1. make/leave additional base ValueObject class for objects with collections as members, then see Solution: 2
//Solution: 2. Exclude collection in GetAtomicValues() abstract overridden method manually
//Solution: 3. Try to resolve with wrapper class that contains any collections. The wrapper class itself  shouldn’t  inherit abstract ValueObject base class.
//Solution: 4. Modify ValueObject<T>  GetHashCode() so that members of any collection Type don’t extract hashcodes. 
//E.g var fields = GetFields(this); //where(field => field.IsNotOfAnyCollectionType)


