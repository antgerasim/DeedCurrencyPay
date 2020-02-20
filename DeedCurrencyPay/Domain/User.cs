using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public class User : IEquatable<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Account Account { get; set; }
        // public IList<Account> Accounts { get; set; }?

        public IList<CurrencyEnum> Currencies { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as User);
        }

        public bool Equals(User other) //реализация IEquatable<Money>
        {
            return other != null && this.Id == other.Id && this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Id} {this.Name}";
        }
    }
}
