using DeedCurrencyPay.Domain;

namespace DeedCurrencyPay.API.Services
{
    public interface IUserService
    {
        User GetById(long id);
    }
}