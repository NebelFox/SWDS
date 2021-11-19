using System.Collections.Generic;
using Shop.Models.Delivery;

namespace Shop.Models.Ordering.Queue
{
    public interface IEnquable
    {
        OrderID Enqueue(IEnumerable<OrderItem> items, DeliveryRequest delivery);
    }
}
