using System.Collections.Generic;
using Shop.Models.Users;

namespace Shop.Views.Authorization
{
    public class UsersRegistry : IAccountsRegistry
    {
        private IDictionary<string, User> _users;

        public UsersRegistry()
        {
            _users = new Dictionary<string, User>();
        }

        public void Add(User user)
        {
            _users.Add(user.Login, user);
        }

        public User Get(string login)
        {
            return _users.TryGetValue(login, out User user) ? user : null;
        }
    }
}
