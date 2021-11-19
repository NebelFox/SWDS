using System.Collections.Generic;
using Shop.Models.Delivery;
using Shop.Models.Ordering;

namespace Shop.Models.Users.Customer
{
    public class Customer
    {
        private IList<OrderID> _orders;

        public Customer(Receiver receiver,
                        CustomerStatus status,
                        string password)
        {
            Receiver = receiver;
            _orders = new List<OrderID>();
            Status = status;
        }

        public CustomerStatus Status;

        public Receiver Receiver;

        public void AppendOrdersHistory(OrderID id)
        {
            _orders.Add(id);
        }
    }
}
