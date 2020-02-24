using DeedCurrencyPay.API.ViewModels;
using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Domain.Common;
using DeedCurrencyPay.Domain.UserAggregate;
using System;

namespace DeedCurrencyPay.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly ICurrencyService currencyService;
        private readonly IUserRepository userRepository;
        public AccountService(IUserRepository userRepository, ICurrencyService currencyService)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService)); ;
        }

        public ResponseVm ConvertToCurrency(int userId, string targetCurrency)
        {
            var user = userRepository.GetById(userId);

            if (targetCurrency == user.Account.Balance.SelectedCurrency.ToString())
            {
                throw new ArgumentException("Невозможно конвертировать в одинаковую валюту ");
            }
            var conversionAmount = currencyService.GetConversionAmount(user.Account.Balance.SelectedCurrency, Currency.Parse(targetCurrency), user.Account.Balance.Amount);
            var account = user.Account.ConvertToCurrency(Currency.Parse(targetCurrency.ToUpper()), conversionAmount);

            return CreateResponseVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, account.ToString());
        }

        public ResponseVm Deposit(int userId, decimal amount)//make async
        {
            var user = userRepository.GetById(userId);
            user.Account.Deposit(new Money(amount, user.Account.Balance.SelectedCurrency));
            var responseMsg = $"Кошелек пополнен на:  {user.Account.Balance.ToString()}.";

            return CreateResponseVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        public ResponseVm GetAccountInfo(int userId)
        {
            var user = userRepository.GetById(userId);
            var accountInfo = user.Account.GetAccountInfo(GetConvertedMoneyCollection(user.Account));
            var responseMsg = $"{accountInfo.ToString()}.";

            return CreateResponseVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        public ResponseVm Withdraw(int userId, decimal amount)
        {
            var user = userRepository.GetById(userId);
            user.Account.Withdraw(new Money(amount, user.Account.Balance.SelectedCurrency));
            var responseMsg = $"Снятие наличных. Баланс {user.Account.Balance.ToString()}.";

            return CreateResponseVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        private IValueObjectCollection<Money> GetConvertedMoneyCollection(Account account)
        {
            var moneyCollection = new ValueObjectCollection<Money>();
            foreach (var targetCurrency in account.Currencies)
            {
                //ToDo Domain Policy Validation Pattern
                if (targetCurrency == account.Balance.SelectedCurrency)
                {
                    continue;
                }
                var conversionResult = currencyService.GetConversionAmount(account.Balance.SelectedCurrency, targetCurrency, account.Balance.Amount);
                moneyCollection.Add(new Money(conversionResult.ConvertedAmountValue, conversionResult.CurrencyTo));
            }
            return moneyCollection;
        }

        private ResponseVm CreateResponseVm(decimal accountBalance, Currency accountCurrency, string message)
        {
            return new ResponseVm() { Balance = accountBalance, Currency = accountCurrency, Message = message };
        }
    }
}

