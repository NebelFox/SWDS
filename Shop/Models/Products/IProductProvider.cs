namespace Shop.Models.Products
{
    public interface IProductProvider
    {
        void Handle(Request request);
    }
}
