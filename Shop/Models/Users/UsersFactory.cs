namespace Shop.Models.Users
{
    public class UsersFactory
    {
        private static UsersFactory _instance;

        private int _nextID;
        
        private UsersFactory()
        {
            _nextID = 0;
        }

        public static UsersFactory Instance => _instance ??= new UsersFactory();

        public User Create(string login, string password, Role role = Role.Customer)
        {
            return new User(_nextID++, login, password, role);
        }
    }
}
