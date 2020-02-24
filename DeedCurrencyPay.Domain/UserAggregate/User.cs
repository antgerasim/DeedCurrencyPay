using DeedCurrencyPay.Domain.Common;


namespace DeedCurrencyPay.Domain
{
    public class User : IEntity<User>, IAggregateRoot
    {        
        public string Name { get; private set; }
        public Account Account { get; private set; }

        public long Id  { get; private set; }

        public User(long id, string name, Account account)
        {
            Id = id;
            Name = name;
            Account = account;
           
        }

        public override string ToString()
        {
            return $"{Id} {this.Name}";
        }
    }
}
