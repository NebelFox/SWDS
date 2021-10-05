using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shop.Models;

namespace Shop.Interactors
{
    public class ExpiresReporter
    {
        private readonly string _filepath;

        public ExpiresReporter(string filepath)
        {
            _filepath = filepath;
        }

        public void Report(DateTime today, IEnumerable<Product> expiredProducts)
        {
            using var writer = new StreamWriter(_filepath);

            writer.WriteLine($"[{today}] - {Quantity(expiredProducts, "product")} expired.");

            foreach (Product product in expiredProducts)
                writer.WriteLine(product);

            writer.WriteLine();
        }

        private static string Quantity<T>(IEnumerable<T> enumerable, string noun)
        {
            int count = enumerable.Count();

            return $"{(count > 0 ? count.ToString() : "No")} {noun}{(count == 1 ? string.Empty : "s")}";
        }
    }
}
