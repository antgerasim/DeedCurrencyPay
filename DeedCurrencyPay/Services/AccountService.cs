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

        public AccountInfoVm Deposit(int userId, decimal amount)//make async
        {
            var user = userRepository.GetById(userId);
            user.Account.Deposit(new Money(amount, user.Account._Balance.SelectedCurrency));
            return CreateAccountInfoVm(user.Account._Balance.Amount, user.Account._Balance.SelectedCurrency, "Кошелек пополнен на: ");
        }
        public AccountInfoVm Withdraw(int userId, decimal amount)//replace string with responseVm(Ok, Bad)
        {
            var user = userRepository.GetById(userId);
            user.Account.Withdraw(new Money(amount, user.Account._Balance.SelectedCurrency));
            var responseMsg = $"Снятие наличных на {user.Account._Balance.Amount} {user.Account._Balance.SelectedCurrency}";
            return CreateAccountInfoVm(user.Account._Balance.Amount, user.Account._Balance.SelectedCurrency, responseMsg);
        }

        public AccountInfoVm ConvertCurrency(int userId, string currTo)
        {
            var user = userRepository.GetById(userId);
            var account = user.Account.ConvertCurrency(currTo.ToEnum<Currency>());

            return CreateAccountInfoVm(user.Account._Balance.Amount, user.Account._Balance.SelectedCurrency, account.ToString());
        }

        private AccountInfoVm CreateAccountInfoVm(decimal accountBalance, Currency accountCurrency, string message)
        {
            //var responseMsg = $"{message}{accountBalance}. Валюта: {accountCurrency}";
            return new AccountInfoVm() { Balance = accountBalance, Currency = accountCurrency, Message = message };
        }

    }
}

//todo delete Account Class, replace with AccountService?