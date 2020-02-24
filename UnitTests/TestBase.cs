using DeedCurrencyPay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeedCurrencyPay.Domain.Common;
using DeedCurrencyPay.Infrastructure.Helpers;
using DeedCurrencyPay.API.Services;
using DeedCurrencyPay.Domain.UserAggregate;
using DeedCurrencyPay.Infrastructure.Repositories;

namespace UnitTests
{

    public abstract class TestBase<T>
    {
        protected IUserRepository userRepository;
        protected ICurrencyService currencyService;
        protected IAccountService accountService;
        protected IEnumerable<Currency> defaultThreeAccountCurrencies;
        protected IEnumerable<Currency> defaultOneAccountCurrency;
        protected IEnumerable<User> uniqueUsers;
        protected IEnumerable<User> dupeUsers;
        protected IEnumerable<Account> uniqueAccounts;
        protected IEnumerable<Account> dupeAccounts;
        protected IEnumerable<AccountInfo> uniqueAccountInfoColl;
        protected IEnumerable<AccountInfo> dupeAccountInfoColl;
        protected IEnumerable<Money> uniqueMoneyColl;
        protected IEnumerable<Money> dupeMoneyColl;
        protected IEnumerable<Money> basicTestMoneyList;
        protected IEnumerable<IValueObjectCollection<Money>> uniqueVOCollections;
        protected IEnumerable<IValueObjectCollection<Money>> dupeVOCollections;        

        protected void TestInitializeBase()
        {
            userRepository = new UserRepository();
            currencyService = new CurrencyService();
            accountService = new AccountService(userRepository, currencyService);

            defaultThreeAccountCurrencies = new List<Currency> { Currency.RUB, Currency.USD, Currency.EUR };
            defaultOneAccountCurrency = new List<Currency> { Currency.IDR };

            uniqueUsers = UsersInit.GetAllUsers();
            dupeUsers = UsersInit.GetAllUsersWithDuplicate();

            uniqueAccounts = AccountInit.GetAllAccounts();
            dupeAccounts = AccountInit.GetAllAccountsWithDuplicate();

            uniqueAccountInfoColl = AccountInfoInit.GetAccountInfoList();
            dupeAccountInfoColl = AccountInfoInit.GetAccountInfosWithDuplicate();

            uniqueMoneyColl = MoneyListInit.GetMoneyList1();
            dupeMoneyColl = MoneyListInit.GetMoneyListWithDuplicates();
            basicTestMoneyList = MoneyListInit.GetOperatorTestMoney();

            uniqueVOCollections = ValueObjectCollectionInit.GetValueObjectCollectionList();
            dupeVOCollections = ValueObjectCollectionInit.GetValueObjectCollectionListWithDuplicates();
        }

        protected static bool Can_Add_To_HashSet(IEnumerable<T> list)
        {
            HashSet<T> hashSet = new HashSet<T>();
            var array = list.ToArray();
            
            var hashsetAddResult = true;
            for (int i = 0; i < array.Length; i++)
            {
                var item = array[i];
                hashsetAddResult = hashSet.Add(item);
                if (!hashsetAddResult)
                {
                    break;
                }
            }
            return hashsetAddResult;
        }

        protected static bool Is_Immutable(Type type)
        {
            if (type.IsPrimitive) return true;
            if (type == typeof(string)) return true;
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var isShallowImmutable = fieldInfos.All(f => f.IsInitOnly);
            if (!isShallowImmutable) return false;
            var isDeepImmutable = fieldInfos.All(f => Is_Immutable(f.FieldType));
            return isDeepImmutable;
        }

        protected static bool IsBetween(decimal num, decimal lower, decimal upper, bool inclusive = false)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        protected static void Assert_Currency_Convertion(Currency targetCurrency, Account result)
        {
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Account));
            Assert.IsTrue(result.Balance.Amount != 0);
            Assert.IsTrue(result.Balance.SelectedCurrency == targetCurrency);
        }
    }
}
