using Shop.Models.Users;

namespace Shop.Views.Clients
{
    public abstract class UserClient : Client
    {
        public UserClient(User user)
        {
            User = user;
        }
        
        public User User { get; }

        public void ChangePassword(string old, string @new)
        {
            User.ChangePassword(old, @new);
        }
    }
}
