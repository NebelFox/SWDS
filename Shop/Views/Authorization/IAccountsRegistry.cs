using Shop.Models.Users;

namespace Shop.Views.Authorization
{
    public interface IAccountsRegistry
    {
        public void Add(User user);

        public User Get(string login);
    }
}
