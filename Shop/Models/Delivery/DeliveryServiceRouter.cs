using System;
using System.Collections.Generic;
using Shop.Models.Ordering;
using Shop.Models.Ordering.Queue;

namespace Shop.Models.Delivery
{
    public class DeliveryServiceRouter : IOrderProcessor
    {
        private readonly Dictionary<DeliveryMethod, IDeliveryService> _services;

        public DeliveryServiceRouter()
        {
            _services = new Dictionary<DeliveryMethod, IDeliveryService>();
        }

        public void RegisterService(IDeliveryService service)
        {
            if (_services.TryAdd(service.Method, service) == false)
                throw new ArgumentException($"A {service.Method} is already registered");
        }

        public void Process(Order order)
        {
            _services[order.Delivery.Method].Deliver(order.Delivery,
                () => order.ChangeStatus(OrderStatus.Delivered));
            order.ChangeStatus(OrderStatus.Assigned);
        }
    }
}
