namespace Shop.Models.Ordering.Queue
{
    public interface IProcessorsRegistry
    {
        void AssignProcessor(OrderStatus status, IOrderProcessor processor);
    }
}
