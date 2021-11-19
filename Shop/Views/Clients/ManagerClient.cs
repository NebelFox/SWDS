using Shop.Models.Users;
using Verbox;
using Verbox.Models;

namespace Shop.Views.Clients
{
    public class ManagerClient : UserClient
    {
        public ManagerClient(User user) : base(user)
        { }

        private void SetDiscount(Context context)
        {
            var id = (int)context["product-id"];
            var discount = (float)context["discount"];
        }

        public override BoxBuilder ExtendBox(BoxBuilder builder)
        {
            return builder
                  .Type<float>("float", float.TryParse)
                  .Command(new Command("set", "set discount value for specific product")
                          .Parameters("<product-id:int>",
                                      "<discount:float>")
                          .Action(SetDiscount));
        }
    }
}
