using System;
using Shop.Models.Users;
using Shop.Views.Clients;

namespace Shop.Views.Authorization
{
    public class Authorizer : IAuthorizer
    {
        private IAccountsRegistry _accounts;
        private ClientsFactory _clientsFactory;

        public Authorizer(IAccountsRegistry accounts, 
                          ClientsFactory clientsFactory)
        {
            _accounts = accounts;
            _clientsFactory = clientsFactory;
        }

        public UserClient Authorize(string login, string password)
        {
            User user = _accounts.Get(login);
            if (user == null)
                throw new ArgumentException($"Unknown login: {login}", nameof(login));
            if (user.MatchPassword(password) == false)
                throw new ArgumentException($"Invalid password: {password}", nameof(password));
            return _clientsFactory.Create(user);
        }

        public void Register(string login, string password, Role role = Role.Customer)
        {
            if (_accounts.Get(login) != null)
                throw new InvalidOperationException($"A user with login \"{login}\" is already registered");
            _accounts.Add(UsersFactory.Instance.Create(login, password, role));
        }
    }
}
