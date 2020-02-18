using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    class Frozen : IAccountState
    {
        private readonly Action onUnfreeze;

        public Frozen(Action onUnfreeze)
        {
            this.onUnfreeze = onUnfreeze;
        }
        public IAccountState Close()
        {
            throw new NotImplementedException();
        }

        public IAccountState Deposit(Action addToBalance)
        {
            this.onUnfreeze();
            addToBalance();
            return new Active(this.onUnfreeze);
        }

        public IAccountState Withdraw(Action substractFromBalance)
        {
            this.onUnfreeze();
            substractFromBalance();
            return new Active(this.onUnfreeze);
        }

        public IAccountState Freeze()
        {
            throw new NotImplementedException();
        }

        public IAccountState HolderVerified()
        {
            throw new NotImplementedException();
        }


    }
}
