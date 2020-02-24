using System;
using System.Collections.Generic;
using System.Text;

namespace DeedCurrencyPay.Domain.Common
{
   public interface IRepository<T> where T : IAggregateRoot
    {
    }
}
