using Shop.Models.Products;

namespace Shop.Models.Storing
{
    public interface IReservation
    {
        void Reserve(ProductID pid, int count);
        void Retrieve(ProductID pid, int count);
    }
}
