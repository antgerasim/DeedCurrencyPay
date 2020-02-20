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
            return _users.SingleOrDefault(x => x.Id == id);
        }

        private void InitUsers()
        {

            _users.Add(new User
            {
                Id = 1,
                Name = "Igor",
                Account = new Account(new Money(10000, CurrencyEnum.RUB), 1000101, "Igor")
            });
            _users.Add(new User
            {
                Id = 2,
                Name = "Yulia",
                Account = new Account(new Money(300000, CurrencyEnum.IDR), 1000102, "Yulia")
            });
        }

        /*private void InitUsers()
        {
            _users.Add(new User
            {
                Id = 1,
                Accounts = new List<Account>()
            {
                new Account(new Money(10000, Currency.RUB)),
                new Account(new Money(100, Currency.USD)),
                new Account(new Money(200, Currency.EUR))
            }
            });
            _users.Add(new User
            {
                Id = 2,
                Accounts = new List<Account>()
            {
                new Account(new Money(300000, Currency.IDR))
            }
            });
        }*/
    }
}
