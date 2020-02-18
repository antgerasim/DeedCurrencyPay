using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Helpers;
using DeedCurrencyPay.Repositories;
using DeedCurrencyPay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;

        public AccountService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public AccountInfoVm Deposit(int userId, Decimal amount, string currency)//make async
        {
            //test
            var foo = Currency.USD.ToFriendlyString();
            var user = userRepository.GetById(userId);
            //Todo remove multiple accounts, think about it- нет, все же множество акаунтов, или все же один аккаунт
            //var account= user.Accounts.SingleOrDefault(acc=>acc.Balance.SelectedCurrency.ToFriendlyString() == currency);
            var account = user.Account;

            account.Deposit(new Money(amount, currency.ToEnum<Currency>()));
            //..weiter mit hier
            var responseMsg = $"Кошелек пополнен на {accountBalance} {accountCurrency}";

            return CreateAccountInfoVm (account.Balance.Amount, account.Balance.SelectedCurrency, responseMsg);
        }
        public AccountInfoVm Withdraw(int userId, Decimal amount, string currency)//replace string with responseVm(Ok, Bad)
        {
            var user = userRepository.GetById(userId);
            user.Account.Withdraw(new Money(amount, currency.ToEnum<Currency>()));
            var responseMsg = $"Снятие наличных на {user.Account.Balance.Amount} {user.Account.Balance.SelectedCurrency}";
            return CreateAccountInfoVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        public AccountInfoVm ConvertCurrency(int userId, string currTo, double exgRate)//or remove exgRate and make xml api call from here
        {
            var user = userRepository.GetById(userId);
            var accountInfo = user.Account.ConvertCurrency(user.Account.Balance, currTo.ToEnum<Currency>(), exgRate);
            var responseMsg = $"Снятие наличных на {user.Account.Balance.Amount} {user.Account.Balance.SelectedCurrency}";
            return accountInfo;
        }

        private AccountInfoVm CreateAccountInfoVm(decimal accountBalance, Currency accountCurrency, string message)
        {
            var msg = 
            return new AccountInfoVm() { Balance = accountBalance, Currency = accountCurrency, Message = message };
        }

    }
}

//todo delete Account Class, replace with AccountService?