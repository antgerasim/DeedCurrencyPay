using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Domain.AccountAggregate;
using DeedCurrencyPay.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IEnumerable<Account> _accounts;

        public AccountRepository()
        {
            _accounts = AccountInit.GetAllAccounts();
        }

        public Account GetById(int id)
        {
            return _accounts.SingleOrDefault(x => x.Id == id);
        }
    }
}