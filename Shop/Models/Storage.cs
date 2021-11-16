using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shop.Interactors;
using Shop.Models.Products;

namespace Shop.Models
{
    public class Storage : IEnumerable<Product>
    {//делегат може бути зовнішнім
        public delegate void OnProductExpired(Product product);

        public event OnProductExpired ProductExpired;

        private readonly List<Product> _products;
        public DateTime Today { get; private set; }

        private Storage(DateTime today)
        {
            Today = today;
        }

        public Storage(DateTime today,
                       int capacity = 0) : this(today)
        {
            _products = new List<Product>(capacity);
        }

        public Storage(DateTime today,
                       IEnumerable<Product> products) : this(today)
        {
            _products = new List<Product>(products);
        }

        public int Count => _products.Count;

        public Product this[int index]
        {
            get
            {
                if (!_products[index].IsExpired(Today))
                    return _products[index];
                ProductExpired?.Invoke(_products[index]);
                return this[index];
            }
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Add(IEnumerable<Product> products)
        {
            _products.AddRange(products);
        }

        public int FindIndex(Predicate<Product> predicate)
        {
            return _products.FindIndex(predicate);
        }

        public Product FindFirst(Predicate<Product> predicate)
        {
            return _products.Find(predicate);
        }

        public IEnumerable<Product> FindAll(Func<Product, bool> predicate)
        {
            return _products.Where(predicate);
        }

        public void Remove(Product product)
        {
            _products.Remove(product);
        }

        public void RemoveFirst(Predicate<Product> predicate)
        {
            int index = FindIndex(predicate);
            if (index != -1)
                RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            _products.RemoveAt(index);
        }

        public IEnumerable<Product> GetAllByType<TProduct>() where TProduct : Product
        {
            return _products.Where(product => product is TProduct);
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
            Today = Today.AddDays(1);
            for (var i = 0; i < _products.Count; i++)
            {
                Product product = _products[i];
            }
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return new StorageEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class StorageEnumerator : IEnumerator<Product>
        {
            private readonly Storage _storage;
            private int _index;

            public StorageEnumerator(Storage storage)
            {
                _storage = storage;
                Reset();
            }

            public bool MoveNext() => ++_index < _storage.Count;

            public void Reset() => _index = -1;

            public Product Current => _storage[_index];

            object IEnumerator.Current => Current;

            public void Dispose()
            { }
        }
    }
}
