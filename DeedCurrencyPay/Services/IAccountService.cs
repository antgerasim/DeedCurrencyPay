using DeedCurrencyPay.ViewModels;

namespace DeedCurrencyPay.Services
{
    public interface IAccountService
    {
        ResponseVm ConvertToCurrency(int userId, string currTo);
        ResponseVm Deposit(int userId, decimal amount);
        ResponseVm Withdraw(int userId, decimal amount);
    }
}