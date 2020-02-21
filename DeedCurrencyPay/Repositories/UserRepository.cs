using DeedCurrencyPay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IList<User> _users;


        public UserRepository()
        {
            _users = new List<User>();
            InitUsers();
        }

        public User GetById(int id)
        {
            return _users.SingleOrDefault(x => x._Id == id);
        }

        private void InitUsers()
        {
            _users.Add(new User(100001, "Igor", new Account(new Money(10000, Currency.RUB), 1000101, "Igor"), 
                new List<Currency> { Currency.RUB, Currency.USD, Currency.EUR }));
            _users.Add(new User(100002, "Yulia", new Account(new Money(300000, Currency.IDR), 1000102, "Yulia"), 
                new List<Currency> { Currency.IDR }));
        }
    }
}
