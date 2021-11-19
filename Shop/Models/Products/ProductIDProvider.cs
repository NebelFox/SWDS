namespace Shop.Models.Products
{
    public class ProductIDProvider
    {
        private static ProductIDProvider _instance;

        private ulong _next;
        
        private ProductIDProvider()
        {
            _next = 0;
        }

        public static ProductIDProvider Instance => _instance ??= new ProductIDProvider();

        public ProductID Next()
        {
            return new ProductID(_next++);
        }
    }
}
