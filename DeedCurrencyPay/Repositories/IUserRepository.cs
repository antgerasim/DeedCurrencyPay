using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Repositories
{
    interface IUserRepository
    {
        User GetById(int id);
    }
}
