using System;
using System.Collections.Generic;
using Shop.Models.Delivery;

namespace Shop.Models.Ordering.Queue
{
    public class OrdersQueue : IEnquable, IProcessorsRegistry
    {
        private readonly IDictionary<OrderID, Order> _orders;
        private readonly Dictionary<OrderStatus, IOrderProcessor> _processors;

        public OrdersQueue()
        {
            _processors = new Dictionary<OrderStatus, IOrderProcessor>();
            _orders = new Dictionary<OrderID, Order>();
        }

        public void AssignProcessor(OrderStatus status, IOrderProcessor processor)
        {
            if (_processors.TryAdd(status, processor) == false)
                throw new ArgumentException($"A processor for {status} is already registered");
        }

        public OrderID Enqueue(IEnumerable<OrderItem> items, DeliveryRequest delivery)
        {
            OrderID oid;
            _orders.Add(oid, new Order(oid, OrderStatus.New, items, delivery));
            Process(oid);
            return oid;
        }

        private void Process(OrderID oid)
        {
            Order order = _orders[oid];
            if (_processors.TryGetValue(order.Status, out IOrderProcessor processor) == false)
                throw new KeyNotFoundException($"No processor found for \"{order.Status}\" order");
            processor.Process(order);
        }

        public OrderStatus this[OrderID oid] => _orders[oid].Status;
    }
}
