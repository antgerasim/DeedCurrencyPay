using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    interface IAccountState
    {
        IAccountState Deposit(Action addToBalance);
        IAccountState Withdraw(Action substractFromBalance);
        IAccountState Freeze();
        IAccountState HolderVerified();
        IAccountState Close(); 

    }
}

