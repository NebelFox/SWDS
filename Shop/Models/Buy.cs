using System;

namespace Shop.Models
{
    public class Buy
    {
        private int _count;

        public Buy()
        {
            Count = 0;
        }

        public Buy(Product product)
        {
            Product = product;
            Count = 1;
        }

        public Buy(Product product, int count)
        {
            Product = product;
            Count = count;
        }

        public Product Product { get; }

        public int Count
        {
            get => _count;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Count),
                                                          value,
                                                          "Count can't be negative");
                _count = value;
            }
        }

        public float Price => Product.Price * Count;
        public double Weight => Product.Weight * Count;

        public override string ToString()
        {
            return $"{Count} x {Product}";
        }
    }
}
