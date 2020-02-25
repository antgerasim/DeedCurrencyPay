using DeedCurrencyPay.Domain.Common;


namespace DeedCurrencyPay.Domain
{
    public class User : Entity<User>, IAggregateRoot
    {        
        public string Name { get; private set; }
        public Account Account { get; private set; }

        public User(long id, string name, Account account)
        {
            base.Id = id;
            Name = name;
            Account = account;
           
        }

        public override string ToString()
        {
            return $"{Id} {this.Name}";
        }
    }
}
