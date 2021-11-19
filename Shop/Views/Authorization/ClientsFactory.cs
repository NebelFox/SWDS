using System;
using System.Collections.Generic;
using System.Globalization;
using Shop.Models.Users;
using Shop.Views.Clients;
using Verbox;

namespace Shop.Views.Authorization
{
    public class ClientsFactory
    {
        private Dictionary<Role, Func<User, UserClient>> _userClientFactories;

        public ClientsFactory()
        {
            _userClientFactories = new Dictionary<Role, Func<User, UserClient>>();
        }

        public void RegisterFactory<TClient>(Role role,
                                             Func<User, TClient> factory)
            where TClient : UserClient
        {
            if (_userClientFactories.TryAdd(role, factory) == false)
                throw new ArgumentException($"A factory for {role} is already registered");
        }

        public UserClient Create(User user)
        {
            return _userClientFactories[user.Role].Invoke(user);
        }

        public Client Create()
        {
            return new GuestClient();
        }

        public static BoxBuilder GetClientBoxBuilderPrefab(User user)
        {
            string role = user.Role.ToString().ToLower();
            return new BoxBuilder()
                  .Type<int>("int", int.TryParse)
                  .Type("float",
                        (string token, out float value) => float.TryParse(token,
                                                                          NumberStyles.Number,
                                                                          CultureInfo.InvariantCulture,
                                                                          out value))
                  .Style(Style.Default with
                   {
                       DialogueGreeting = $"Welcome. You are logged as {role}",
                       DialoguePromptIndicator = $"[{user.Login}:{role}]~$ ",
                       DialogueFarewell = "Logging out..."
                   })
                  .Command(new Command("change-password", null)
                          .Parameters("<current-password>",
                                      "<new-password>")
                          .Action(context =>
                           {
                               user.ChangePassword((string)context["current-password"],
                                                   (string)context["new-password"]);
                           }))
                  .Command(new Command("logout", "return to authorization lobby")
                              .Action(context => context.Box.Terminate()));
        }
    }
}
