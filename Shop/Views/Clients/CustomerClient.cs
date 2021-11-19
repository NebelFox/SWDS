using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Models;
using Shop.Models.Delivery;
using Shop.Models.Ordering;
using Shop.Models.Products;
using Shop.Models.Storing;
using Shop.Models.Users;
using Shop.Views.Assortment;
using Shop.Views.Common;
using Verbox;
using Verbox.Models;

namespace Shop.Views.Clients
{
    public class CustomerClient : UserClient
    {
        private readonly TableViewer<ProductID> _catalogViewer;
        private readonly ShoppingCart _cart;
        private readonly TableViewer<CartItem> _cartViewer;

        public CustomerClient(User user) : base(user)
        {
            IStorage storage = ShopFacade.Instance.Storage;
            var catalogColumns = new (string, Selector<ProductID>)[]
            {
                ("Name", id => storage[id].Product.Name),
                ("Price", id => storage[id].Product.Price),
                ("Count", id => storage[id].Available)
            };
            _catalogViewer = new TableViewer<ProductID>(storage.ToList,
                                                        catalogColumns);
            storage.Subscribe(UpdateCatalogViewer);

            _cart = ShopFacade.Instance.GetCart();
            var cartColumns = new (string, Selector<CartItem>)[]
            {
                ("Name", item => storage[item.Pid].Product.Name),
                ("Price", item => storage[item.Pid].Product.Price),
                ("Count", item => item.Count),
                ("Total", item => storage[item.Pid].Product.Price * item.Count)
            };
            _cartViewer = new TableViewer<CartItem>(() => _cart,
                                                    cartColumns);
            _cart.Subscribe(UpdateCartViewer);
        }

        private void View(Context context)
        {
            foreach (string row in _catalogViewer.View())
                Console.WriteLine(row);
        }

        private void ViewCart(Context context)
        {
            IStorage storage = ShopFacade.Instance.Storage;
            foreach (string row in _cartViewer.View())
                Console.WriteLine(row);

            float cost = _cart.Sum(item => storage[item.Pid].Product.Price * item.Count);
            Console.WriteLine($"Total cost: {cost}");
        }

        private void AddToCart(Context context)
        {
            IStorage storage = ShopFacade.Instance.Storage;
            var index = (int)context["index"];
            if (index > _catalogViewer.Count)
                throw new ArgumentException($"Index out of bounds: {index}");
            var count = (int)context["count"];
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Must be >= 1");
            ProductID pid = _catalogViewer[index];
            storage.Reserve(pid, count);
            _cart.Add(pid, count);
            Console.WriteLine($"Added {count} {storage[pid].Product.Name} to the cart");
        }

        private void RemoveFromCart(Context context)
        {
            var index = (int)context["index"];
            var count = (int)context["count"];
            RemoveFromCart(index, count);
        }

        private void RemoveFromCart(int index, int count = -1)
        {
            if (index > _cart.Count)
                throw new ArgumentException($"Index out of cart cart content bounds: {index}");
            if (count > _cart[index].Count)
                throw new ArgumentException("Tried to remove more than the cart holds");
            if (count == -1)
                count = _cart[index].Count;
            IStorage storage = ShopFacade.Instance.Storage;
            (ProductID pid, _) = _cart[index];
            storage.Retrieve(pid, count);
            _cart.Remove(index);
            Console.WriteLine($"Removed {count} {storage[pid].Product.Name} from the cart");
        }

        private void ClearCart(Context context)
        {
            for (int i = _cart.Count; i != 0; --i)
                RemoveFromCart(i - 1);
        }

        private void Order(Context context)
        {
            IStorage storage = ShopFacade.Instance.Storage;
            var receiver = new Receiver((string)context["receiver-full-name"],
                                        (string)context["receiver-phone-number"]);
            var address = (string)context["address"];
            var delivery = (DeliveryMethod)context["delivery"];

            var orderItems = new List<OrderItem>();
            for (int index = _cart.Count; index != 0; --index)
            {
                (ProductID pid, int count) = _cart[index];
                RemoveFromCart(index - 1);
                storage.Take(pid, count);
                orderItems.Add(new OrderItem(pid, count, storage[pid].Product.Price * count));
            }

            ShopFacade.Instance.OrdersQueue.Enqueue(orderItems, new DeliveryRequest(delivery, address, receiver));
        }

        private void UpdateCatalogViewer()
        {
            _catalogViewer.Update();
        }

        private void UpdateCartViewer()
        {
            _cartViewer.Update();
        }

        public override BoxBuilder ExtendBox(BoxBuilder builder)
        {
            return builder
                  .Type<DeliveryMethod>("delivery-service")
                  .Command(new Command("view", "the catalog")
                          .Parameters("--count <n:int>=-1",
                                      "--start <index:int>=0")
                          .Action(View))
                  .Command(new Command("view-cart", "self-explaining")
                              .Action(ViewCart))
                  .Command(new Command("pick", "a product and add to the cart")
                          .Parameters("<index:int>",
                                      "<count:int>")
                          .Action(AddToCart))
                  .Command(new Command("drop", "a picked product from the cart")
                          .Parameters("<index:int>",
                                      "--count <n:int>=-1")
                          .Action(RemoveFromCart))
                  .Command(new Command("drop-all", "the picked products from the cart")
                              .Action(ClearCart))
                  .Command(new Command("order", "all the picked products")
                          .Parameters("<receiver-full-name>",
                                      "<receiver-phone-number>",
                                      "--delivery <service:delivery-service>=self-pickup",
                                      "<address>")
                          .Action(Order));
        }

        ~CustomerClient()
        {
            ShopFacade.Instance.Storage.Unsubscribe(UpdateCatalogViewer);
            _cart.Unsubscribe(UpdateCartViewer);
        }
    }
}
