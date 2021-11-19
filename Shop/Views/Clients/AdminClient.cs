using System;
using Shop.Models;
using Shop.Models.Products;
using Shop.Models.Storing;
using Shop.Models.Users;
using Shop.Views.Common;
using Verbox;
using Verbox.Models;

namespace Shop.Views.Clients
{
    public class AdminClient : UserClient
    {
        private readonly TableViewer<ProductID> _catalogViewer;

        public AdminClient(User user) : base(user)
        {
            IProductsRegistry storage = ShopFacade.Instance.Storage;
            var columns = new (string, Selector<ProductID>)[]
            {
                ("PID", pid => pid),
                ("Name", pid => storage[pid].Product.Name),
                ("Price", pid => storage[pid].Product.Price),
                ("Available", pid => storage[pid].Available),
                ("Reserved", pid => storage[pid].Reserved),
                ("Count", pid => storage[pid].Available)
            };

            _catalogViewer = new TableViewer<ProductID>(ShopFacade.Instance.Storage.ToList,
                                                columns);
            storage.Subscribe(UpdateCatalogViewer);
        }

        private void View(Context context)
        {
            foreach (string row in _catalogViewer.View())
                Console.WriteLine(row);
        }

        private void Create(Context context)
        {
            var name = (string)context["name"];
            var price = (float)context["price"];
            ProductID id = ShopFacade.Instance.Storage.RegisterProduct(new Product(name, price));
            if((bool)context["silent"] == false)
                Console.WriteLine($"\"{name}\" ID: {{ {id} }}");
        }

        private void Remove(Context context)
        {
            var index = (int)context["index"];
            ProductID id = _catalogViewer[index];
            ShopFacade.Instance.Storage.RemoveProduct(id);
        }

        private void Edit(Context context)
        {
            var index = (int)context["index"];
            ProductID pid = _catalogViewer[index];
            Product product = ShopFacade.Instance.Storage[pid].Product;
            if (context["name"] is string name)
                product.Name = name;
            if (context["price"] is float price)
                product.Price = price;
        }

        private void Request(Context context)
        {
            var index = (int)context["index"];
            ProductID pid = _catalogViewer[index];
            var count = (int)context["count"];
            ShopFacade.Instance.Storage.Replenish(pid, count);
        }

        private void Reduce(Context context)
        {
            var index = (int)context["index"];
            ProductID pid = _catalogViewer[index];
            var count = (int)context["count"];
            ShopFacade.Instance.Storage.Take(pid, count);
        }

        private static void Register(Context context)
        {
            var login = (string)context["login"];
            var password = (string)context["password"];
            var role = (Role)context["role"];
            ShopFacade.Instance.Authorizer.Register(login, password, role);
        }

        private void UpdateCatalogViewer()
        {
            _catalogViewer.Update();
        }

        public override BoxBuilder ExtendBox(BoxBuilder builder)
        {
            return builder
                  .Type<Role>()
                  .Command(new Command("view", "list the storage content")
                          .Parameters("--count <n:int>=-1",
                                      "--start <index:int>=0")
                          .Action(View))
                   
                  .Command(new Command("create", "register a new product in the storage")
                          .Parameters("<name>",
                                      "<price:float>",
                                      "--silent")
                          .Action(Create))
                   
                  .Command(new Command("remove", "unregister a product from the storage")
                          .Parameters("<index:int>")
                          .Action(Remove))
                   
                  .Command(new Command("edit", "adjust some properties of a product")
                          .Parameters("<index:int>",
                                      "--name <name>",
                                      "--price <value:float>")
                          .Action(Edit))
                   
                  .Command(new Command("request", "request some amount of the product instances to the storage")
                          .Parameters("<index:int>",
                                      "<count:int>")
                          .Action(Request))
                   
                  .Command(new Command("reduce", "decrease the amount of the product instances in the storage")
                          .Parameters("<index:int>",
                                      "<count:int>")
                          .Action(Reduce))
                   
                  .Command(new Command("register", "register a new account with specific role")
                          .Parameters("<login>",
                                      "<password>",
                                      "--role <role:role>=customer")
                          .Action(Register));
        }
        
        ~AdminClient()
        {
            ShopFacade.Instance.Storage.Unsubscribe(UpdateCatalogViewer);
        }
    }
}
