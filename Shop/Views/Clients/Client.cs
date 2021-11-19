using Verbox;

namespace Shop.Views.Clients
{
    public abstract class Client
    {
        public abstract BoxBuilder ExtendBox(BoxBuilder builder);
    }
}
