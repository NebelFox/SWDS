using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task.Models
{
    public class Storage : IEnumerable<Product>
    {
        private readonly List<Product> _products;

        public Storage() : this(0)
        { }

        public Storage(int initialCapacity)
        {
            _products = new List<Product>(initialCapacity);
        }

        public Storage(IEnumerable<Product> products)
        {
            _products = new List<Product>(products);
        }

        public int Count => _products.Count;

        public Product this[int index] => _products[index];

        public IEnumerator<Product> GetEnumerator()
        {
            return _products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Add(IEnumerable<Product> products)
        {
            _products.AddRange(products);
        }

        public IEnumerable<Product> GetAllMeatProducts()
        {
            return _products.Where(product => product is Meat);
        }

        public void View()
        {
            if (Count > 0)
            {
                Console.WriteLine("Storage currently contains:");
                var index = 0;
                foreach (Product product in _products)
                    Console.WriteLine($"{++index}. {product}");
            }
            else
            {
                Console.WriteLine("Storage currently is empty");
            }
        }

        public void ChangePrice(int percents)
        {
            foreach (Product product in _products)
                product.ChangePrice(percents);
        }
    }
}
