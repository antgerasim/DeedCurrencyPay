using DeedCurrencyPay.API.ViewModels;

namespace DeedCurrencyPay.API.Services
{
    public interface IAccountService
    {
        ResponseVm ConvertToCurrency(long userId, string currTo);
        ResponseVm Deposit(long userId, decimal amount);
        ResponseVm Withdraw(long userId, decimal amount);
        ResponseVm GetAccountInfo(long userId);
    }
}