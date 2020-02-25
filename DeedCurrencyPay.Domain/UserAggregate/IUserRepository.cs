using DeedCurrencyPay.Domain.Common;

namespace DeedCurrencyPay.Domain.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        User GetById(long id);
    }
}