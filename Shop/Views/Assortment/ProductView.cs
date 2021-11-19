namespace Shop.Views.Assortment
{
    public class ProductView
    {
        private Models.Products.Product _product;
        
        public ProductView(Models.Products.Product product)
        {
            _product = product;
        }

        public string Name => _product.Name;

        public float Price => _product.Price;

        public override string ToString() => $"{Name} for {Price}";
    }
}
