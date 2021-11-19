using Shop.Models.Products;

namespace Shop.Models.Ordering
{
    public record OrderItem(ProductID Pid, int Count, float TotalCost);
}
