namespace Shop.Models.Ordering.Queue
{
    public interface IOrderProcessor
    {
        void Process(Order order);
    }
}
