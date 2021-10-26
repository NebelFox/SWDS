using System;
using System.Collections.Generic;
using System.IO;
using Shop.Models;
using Shop.Utils;

namespace Shop.Interactors
{
    public class ProductsDeserializer
    {
        public delegate void OnLineFailed(int lineIndex, Exception exception);

        public event OnLineFailed LineFailed;

        public IEnumerable<Product> DeserializeFile(string filepath)
        {
            var products = new List<Product>();
            using var reader = new StreamReader(filepath);
            var i = 0;
            foreach (string line in reader.ReadLines())
            {
                try
                {
                    products.Add(ProductParser.Parse(line));
                }
                catch (Exception e)
                {
                    LineFailed?.Invoke(i, e);
                }
                ++i;
            }
            return products;
        }
    }
}
