using System;
using Shop.Models.Products;

namespace Shop.Models.Storing
{
    public class ProductStack
    {
        public ProductStack(Product product, int count = 0)
        {
            Product = product;
            Available = count;
        }

        public Product Product { get; }

        public int Available { get; private set; }
        
        public int Reserved { get; private set; }

        public int Count => Available + Reserved;

        public void Add(int count)
        {
            Available += count;
        }

        public void Remove(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count),
                                                      count,
                                                      "Must be non-negative");
            Available -= count;
        }

        public void Reserve(int count)
        {
            Available -= count;
            Reserved += count;
        }

        public void Retrieve(int count)
        {
            Reserved -= count;
            Available += count;
        }
    }
}
