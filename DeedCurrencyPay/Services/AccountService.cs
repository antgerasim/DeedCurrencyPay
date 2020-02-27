using DeedCurrencyPay.API.ViewModels;
using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Domain.Common;
using DeedCurrencyPay.Domain.UserAggregate;
using System;
using System.Threading.Tasks;

namespace DeedCurrencyPay.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly ICurrencyService currencyService;
        private readonly IUserService userService;

        public AccountService(IUserService userService, ICurrencyService currencyService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService)); ;
        }

        public ResponseVm ConvertToCurrency(long userId, string targetCurrencyStr)
        {
            //3 c. Перевести деньги из одной валюты в другую
            var user = userService.GetById(userId);
            var targetCurrency = Currency.Parse(targetCurrencyStr);
            var fromCurrency = user.Account.Balance.SelectedCurrency;
            if (targetCurrency == fromCurrency)
            {
                throw new ArgumentException("Невозможно конвертировать в одинаковую валюту ");
            }

            var conversionAmount = currencyService.GetConversionAmount(user.Account.Balance.SelectedCurrency, targetCurrency, user.Account.Balance.Amount);
            user.Account.ConvertToCurrency(targetCurrency, conversionAmount);

            var responseMsg = $"Конвертация валюты с {fromCurrency} в {targetCurrency.ToString()}. Баланс: {user.Account.Balance.ToString()}.";

            return CreateResponseVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        public ResponseVm Deposit(long userId, decimal amount)
        {
            //3 a. Пополнить кошелек в одной из валют
            var user = userService.GetById(userId);
            user.Account.Deposit(new Money(amount, user.Account.Balance.SelectedCurrency));

            var responseMsg = $"Кошелек пополнен на: {amount} {user.Account.Balance.SelectedCurrency}. Баланс: {user.Account.Balance.ToString()}.";

            return CreateResponseVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        public ResponseVm GetAccountInfo(long userId)
        {
            //3 d. Получить состояние своего кошелька (сумму денег в каждой из валют)
            var user = userService.GetById(userId);
            var moneyCollection = GetConvertedMoneyCollection(user.Account);
            var responseMsg = new AccountInfo(user.Account.Balance, moneyCollection).ToString();

            return CreateResponseVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        public ResponseVm Withdraw(long userId, decimal amount)
        {
            //3 b. Снять деньги в одной из валют
            var user = userService.GetById(userId);
            user.Account.Withdraw(new Money(amount, user.Account.Balance.SelectedCurrency));
            var responseMsg = $"Снятие денег на: {amount} {user.Account.Balance.SelectedCurrency}. Баланс: {user.Account.Balance.ToString()}.";

            return CreateResponseVm(user.Account.Balance.Amount, user.Account.Balance.SelectedCurrency, responseMsg);
        }

        private ResponseVm CreateResponseVm(decimal accountBalance, Currency accountCurrency, string message)
        {
            return new ResponseVm() { Amount = accountBalance, Currency = accountCurrency, Message = message };
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
                moneyCollection = (ValueObjectCollection<Money>)moneyCollection.AddImmutable(new Money(conversionResult.ConvertedAmountValue, conversionResult.CurrencyTo));
            }
            return moneyCollection;           
        }
    }
}