using System;
using Shop.Models;
using Verbox;
using Verbox.Models;

namespace Shop.Views.Clients
{
    public class GuestClient : Client
    {
        private static void Login(Context context)
        {
            Console.WriteLine();
            ShopFacade.Instance.Authorize((string)context["login"],
                                          (string)context["password"])
                      .StartDialogue();
        }

        private static void Register(Context context)
        {
            ShopFacade.Instance.Authorizer.Register((string)context["login"],
                                                    (string)context["password"]);
        }

        public override BoxBuilder ExtendBox(BoxBuilder builder)
        {
            return builder
                  .Command(new Command("login", "authorize via specific credentials")
                          .Parameters("<login>",
                                      "<password>")
                          .Action(Login))
                   
                  .Command(new Command("register", "register a new customer account via specific credentials")
                          .Parameters("<login>",
                                      "<password>")
                          .Action(Register))
                   
                  .Command(new Command("exit", "terminate the program")
                              .Action(context => context.Box.Terminate()))
                   
                  .Style(Style.Default with
                   {
                       DialogueGreeting = "You are in authorization lobby",
                       DialoguePromptIndicator = "[lobby]~$ "
                   });
        }
    }
}
