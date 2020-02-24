using DeedCurrencyPay.API.ViewModels;

namespace DeedCurrencyPay.API.Services
{
    public interface IAccountService
    {
        ResponseVm ConvertToCurrency(int userId, string currTo);
        ResponseVm Deposit(int userId, decimal amount);
        ResponseVm Withdraw(int userId, decimal amount);
    }
}