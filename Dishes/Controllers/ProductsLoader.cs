using System;
using System.Collections.Generic;
using System.IO;
using Dishes.Models;
using Dishes.Utils;

namespace Dishes.Controllers
{
    public static class ProductsLoader
    {
        public static IEnumerable<Product> LoadFromFile(string filepath)
        {
            FilepathUtils.ValidateFilepath(filepath);

            using var reader = new StreamReader(filepath);
            var products = new List<Product>();
            var lineIndex = 0;
            while (!reader.EndOfStream)
            {
                ++lineIndex;
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line) == false)
                    products.Add(ParseProduct(line, lineIndex));
            }
            return products;
        }

        private static Product ParseProduct(string line, int lineIndex)
        {
            string[] tokens = line.Split();

            if (string.IsNullOrWhiteSpace(tokens[0]))
                throw new FormatException($"Line {lineIndex}: product name can't be empty");

            if (!double.TryParse(tokens[1], out double price))
                throw new FormatException($"Line {lineIndex}: expected double as product price, got '{price}'");

            return new Product(tokens[0], price);
        }
    }
}
