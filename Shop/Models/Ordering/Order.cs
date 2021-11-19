using System.Collections.Generic;
using System.Linq;
using Shop.Models.Delivery;

namespace Shop.Models.Ordering
{
    public class Order
    {
        private IEnumerable<OrderItem> _items;

        public Order(OrderID id, 
                     OrderStatus status, 
                     IEnumerable<OrderItem> items, 
                     DeliveryRequest delivery)
        {
            ID = id;
            Status = status;
            _items = items;
            Delivery = delivery;
        }

        public OrderID ID { get; }

        public DeliveryRequest Delivery { get; }
        
        public OrderStatus Status { get; private set; }

        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
        }

        public float TotalCost => _items.Sum(i => i.TotalCost);
    }
}
