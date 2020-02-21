using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public class User : Entity<User>
    {

        public string Name { get; private set; }
        public Account Account { get; private set; }
        // public IList<Account> Accounts { get; set; }?
        //public IEnumerable<Currency> Currencies { get; private set; }

        public User(long id, string name, Account account)
        {
            base.Id = id;
            Name = name;
            Account = account;
           
        }

        public override string ToString()
        {
            return $"{base.Id} {this.Name}";
        }
    }
}
