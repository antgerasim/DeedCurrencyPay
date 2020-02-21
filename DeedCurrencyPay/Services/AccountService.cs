using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Helpers;
using DeedCurrencyPay.Repositories;
using DeedCurrencyPay.ViewModels;

namespace DeedCurrencyPay.Services
{
    internal class AccountService : IAccountService
    {
        private readonly ICurrencyService currencyService;
        private readonly IUserRepository userRepository;
        public AccountService(IUserRepository userRepository, ICurrencyService currencyService)
        {
            this.userRepository = userRepository;
            this.currencyService = currencyService;
        }

        public ResponseVm ConvertToCurrency(int userId, string targetCurrency)
        {
            var user = userRepository.GetById(userId);
            var account = user.Account.ConvertToCurrency(Currency.Parse(targetCurrency.ToUpper()), currencyService);

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
            var accountInfo = user.Account.GetAccountInfo(currencyService);
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

        private ResponseVm CreateResponseVm(decimal accountBalance, Currency accountCurrency, string message)
        {
            return new ResponseVm() { Balance = accountBalance, Currency = accountCurrency, Message = message };
        }
    }
}

