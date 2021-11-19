namespace Shop.Models.Products
{
    public class Product
    {
        public Product(string name, float price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get;  set; }

        public float Price { get;  set; }
    }
}
