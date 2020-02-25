using DeedCurrencyPay.Domain.Common;

namespace DeedCurrencyPay.Domain.AccountAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account GetById(int id);
    }
}