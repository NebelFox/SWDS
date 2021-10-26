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

        public void Report(DateTime today, Product expiredProduct)
        {
            using var writer = new StreamWriter(_filepath, true);
            writer.WriteLine($"{today} | {expiredProduct} expired");
        }
    }
}
