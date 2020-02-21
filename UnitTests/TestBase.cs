using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Services;
using DeedCurrencyPay.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DeedCurrencyPay.Helpers;

namespace UnitTests
{
   // [TestClass]
    public abstract class TestBase
    {
        protected ICurrencyService currencyService;
        protected IEnumerable<Currency> defaultThreeAccountCurrencies;
        public IEnumerable<Currency> defaultOneAccountCurrency;
        //IUserRepository userRepository;
        protected IEnumerable<User> uniqueUsers;
        protected IEnumerable<User> dupeUsers;
        protected IEnumerable<Account> uniqueAccounts;
        protected IEnumerable<Account> dupeAccounts;
        protected IEnumerable<Money> uniqueMoneyList;
        protected IEnumerable<Money> dupeMoneyList;

        [TestInitialize]
        protected void TestBaseInitialize()
        {
            currencyService = new CurrencyService();
            defaultThreeAccountCurrencies = new List<Currency> { Currency.RUB, Currency.USD, Currency.EUR };
            defaultOneAccountCurrency = new List<Currency> { Currency.IDR };

            uniqueUsers = UsersInit.GetAllUsers();
            dupeUsers = UsersInit.GetAllUsersWithDuplicate();

            uniqueAccounts = AccountInit.GetAllAccounts();
            dupeAccounts = AccountInit.GetAllAccountsWithDuplicate();

            uniqueMoneyList = MoneyInit.GetMoneyList();
            dupeMoneyList = MoneyInit.GetMoneyListWithDuplicates();
        }
    }
}
