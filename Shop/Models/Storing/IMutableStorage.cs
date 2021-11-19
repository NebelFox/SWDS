using Shop.Models.Products;

namespace Shop.Models.Storing
{
    public interface IMutableStorage : IReadOnlyStorage
    {
        void Replenish(ProductID pid, int count = 0);
        void Take(ProductID pid, int count);
    }
}
