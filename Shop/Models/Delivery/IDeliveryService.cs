using System;

namespace Shop.Models.Delivery
{
    public interface IDeliveryService
    {
        DeliveryMethod Method { get; }
        
        float GetPrice(string address);

        void Deliver(DeliveryRequest request,
                     Action onDelivered);
    }
}
