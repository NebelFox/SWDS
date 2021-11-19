namespace Shop.Models.Delivery
{
    public record DeliveryRequest(DeliveryMethod Method,
                                  string Address,
                                  Receiver Receiver);
}
