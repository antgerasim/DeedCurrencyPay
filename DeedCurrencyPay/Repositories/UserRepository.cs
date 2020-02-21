using DeedCurrencyPay.Domain;
using DeedCurrencyPay.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IEnumerable<User> _users;


        public UserRepository()
        {
            _users = UsersInit.GetAllUsers();
          
        }

        public User GetById(int id)
        {
            return _users.SingleOrDefault(x => x.Id == id);
        }

    }
}
