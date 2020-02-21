using DeedCurrencyPay.ViewModels;

namespace DeedCurrencyPay.Services
{
    public interface IAccountService
    {
        AccountInfoVm ConvertCurrency(int userId, string currTo);
        AccountInfoVm Deposit(int userId, decimal amount);
        AccountInfoVm Withdraw(int userId, decimal amount);
    }
}