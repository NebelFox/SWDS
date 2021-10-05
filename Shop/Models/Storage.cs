using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shop.Interactors;
using Shop.Models.Products;

namespace Shop.Models
{
    public class Storage : IEnumerable<Product>
    {
        private readonly List<Product> _products;
        private DateTime _today;
        private readonly ExpiresReporter _reporter;

        private Storage(DateTime today, ExpiresReporter reporter)
        {
            _today = today;
            _reporter = reporter;
        }

        public Storage(DateTime today, 
                       ExpiresReporter reporter, 
                       int capacity = 0) : this(today, reporter)
        {
            _products = new List<Product>(capacity);
        }

        public Storage(DateTime today, 
                       ExpiresReporter reporter, 
                       IEnumerable<Product> products) : this(today, reporter)
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

        public void DailyUpdate()
        {
            _today = _today.AddDays(1);
            ReportExpired();
            RemoveExpired();
        }

        private void ReportExpired()
        {
            _reporter.Report(_today, _products.Where(p => p.IsExpired(_today)));
        }
        
        private void RemoveExpired()
        {
            _products.RemoveAll(p => p.IsExpired(_today));
        }
    }
}
