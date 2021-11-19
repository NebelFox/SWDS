using Shop.Models.Ordering.Queue;
using Shop.Models.Storing;
using Shop.Models.Users;
using Shop.Views.Assortment;
using Shop.Views.Authorization;
using Shop.Views.Clients;
using Verbox;

namespace Shop.Models
{
    public class ShopFacade
    {
        private static ShopFacade _instance;

        private ClientsFactory _clientsFactory;

        private ShopFacade()
        {
            _clientsFactory = new ClientsFactory();
            _clientsFactory.RegisterFactory(Role.Customer, user => new CustomerClient(user));
            _clientsFactory.RegisterFactory(Role.Manager, user => new ManagerClient(user));
            _clientsFactory.RegisterFactory(Role.Admin, user => new AdminClient(user));
            OrdersQueue = new OrdersQueue();
            Storage = new Storage();
            Authorizer = new Authorizer(new UsersRegistry(), _clientsFactory);
        }

        public static ShopFacade Instance => _instance ??= new ShopFacade();

        public IStorage Storage { get; }

        public IAuthorizer Authorizer { get; }
        
        public IEnquable OrdersQueue { get; }

        public ShoppingCart GetCart() => new ShoppingCart();

        public Box Authorize(string login, string password)
        {
            UserClient client = Authorizer.Authorize(login, password);
            BoxBuilder builder = ClientsFactory.GetClientBoxBuilderPrefab(client.User);
            client.ExtendBox(builder);
            return builder.Build();
        }

        public Box GetGuestClient()
        {
            return  _clientsFactory.Create().ExtendBox(new BoxBuilder()).Build();
        }
    }
}
