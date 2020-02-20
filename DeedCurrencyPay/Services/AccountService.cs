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

        public AccountInfoVm Deposit(int userId, decimal amount, string currency)//make async
        {
            var user = userRepository.GetById(userId);
            user.Account.Deposit(new Money(amount, currency.ToEnum<CurrencyEnum>()));
            return CreateAccountInfoVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, "Кошелек пополнен на: ");
        }
        public AccountInfoVm Withdraw(int userId, decimal amount, string currency)//replace string with responseVm(Ok, Bad)
        {
            var user = userRepository.GetById(userId);
            user.Account.Withdraw(new Money(amount, currency.ToEnum<CurrencyEnum>()));
            var responseMsg = $"Снятие наличных на {user.Account.Balance.Amount} {user.Account.Balance.SelectedCurrency}";
            return CreateAccountInfoVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        public AccountInfoVm ConvertCurrency(int userId, string currTo, double exgRate)//or remove exgRate and make xml api call from here
        {
            var user = userRepository.GetById(userId);
            var accountInfo = user.Account.ConvertCurrency(currTo.ToEnum<CurrencyEnum>(), exgRate);

            return CreateAccountInfoVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, $"Конвертация валюты с {user.Account.Balance.SelectedCurrency.ToFriendlyString()}. Состояние счета: "); ;
        }

        private AccountInfoVm CreateAccountInfoVm(decimal accountBalance, CurrencyEnum accountCurrency, string message)//todo to static class string
        {
            var responseMsg = $"{message}{accountBalance}. Валюта: {accountCurrency}";
            return new AccountInfoVm() { Balance = accountBalance, Currency = accountCurrency, Message = message };
        }

    }
}

//todo delete Account Class, replace with AccountService?