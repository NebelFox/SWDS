using Shop.Models.Users;
using Shop.Views.Clients;

namespace Shop.Views.Authorization
{
    public interface IAuthorizer
    {
        UserClient Authorize(string login, string password);

        public void Register(string login, string password, Role role = Role.Customer);
    }
}
