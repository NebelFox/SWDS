using Shop.Models.Products;

namespace Shop.Models.Storing
{
    public interface IProductsRegistry : IReadOnlyStorage
    {
        ProductID RegisterProduct(Product product);
        
        void RemoveProduct(ProductID pid);
    }
}
