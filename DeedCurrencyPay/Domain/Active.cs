using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
     class Active : IAccountState
    {

        public Action OnUnfreeze { get; }


        public Active(Action onUnfreeze)
        {
            OnUnfreeze = onUnfreeze;
        }

        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Withdraw(Action substractFromBalance)
        {
            substractFromBalance();
            return this;
        }

        public IAccountState Close()
        {
            throw new NotImplementedException();
        }

        public IAccountState HolderVerified()
        {
            return this;
        }



        public IAccountState Freeze() => new Frozen(this.OnUnfreeze);
    }
}
