using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
     class NotVerified : IAccountState
    {
        private Action OnUnfreeze { get; }

        public NotVerified(Action onUnfreeze)
        {
            this.OnUnfreeze = onUnfreeze;
        }
        public IAccountState Close(Action onUnfreeze)
        {
            return new Closed();
        }

        public IAccountState Close()
        {
            throw new NotImplementedException();
        }

        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Freeze()
        {
            return this;
        }

        public IAccountState HolderVerified()
        {
            return new Active(this.OnUnfreeze);
        }

        public IAccountState Withdraw(Action substractFromBalance)
        {
            return this;
        }
    }
}
