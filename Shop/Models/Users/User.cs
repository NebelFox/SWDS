using System;

namespace Shop.Models.Users
{
    public class User
    {
        private string _password;
        
        public User(int id, 
                    string login,
                    string password,
                    Role role)
        {
            Login = login;
            ID = id;
            Role = role;
            _password = password;
        }

        public int ID { get; }

        public string Login { get; }
        
        public Role Role { get; }

        public bool MatchPassword(string password) => password == _password;

        public void ChangePassword(string current, string @new)
        {
            if (MatchPassword(current) == false)
                throw new ArgumentException("Invalid current password", nameof(current));
            _password = @new;
        }
    }
}
