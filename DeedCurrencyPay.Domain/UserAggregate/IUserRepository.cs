using DeedCurrencyPay.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeedCurrencyPay.Domain.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        User GetById(long id);
    }
}
