using System;
using System.Linq;
using Shop.Interactors;
using Shop.Models;
using Shop.Utils;
using Sort;
using Task.Interactors;

namespace Shop
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StorageMatcherDemo();
        }

        private static void StorageMatcherDemo()
        {
            Storage lhs = CreateStorage();
            Storage rhs = CreateStorage();

            Console.WriteLine("Common products:");
            foreach (Product product in StorageMatcher.IntersectProducts(lhs, rhs))
                Console.WriteLine(product);
            
            Console.WriteLine("\nProducts in the first, but not in the second:");
            foreach (Product product in StorageMatcher.Except(lhs, rhs))
                Console.WriteLine(product);
            
            Console.WriteLine("\nDifferent products:");
            foreach (Product product in StorageMatcher.SymmetricExcept(lhs, rhs))
                Console.WriteLine(product);
            
        }

        private static void SortingDemo()
        {
            Storage storage = CreateStorage();

            PrintStorageSorted(storage,
                               (lhs, rhs) => lhs.Price.CompareTo(rhs.Price),
                               "price");

            PrintStorageSorted(storage,
                               (lhs, rhs) => lhs.Name.CompareTo(rhs.Name),
                               "price");

            PrintStorageSorted(storage,
                               (lhs, rhs) => lhs.Weight.CompareTo(rhs.Weight),
                               "weight");
        }

        private static Storage CreateStorage()
        {
            Console.WriteLine("Enter a file to write reports about expired products to:");
            string filepath = Console.ReadLine();
            var reporter = new ExpiresReporter(filepath);

            Console.WriteLine("Enter the starting date for the storage:");
            var today = DateTime.Parse(Console.ReadLine());

            var storage = new Storage(today, reporter);
            storage.Add(ProductCreationPrompter.StartDialog());

            return storage;
        }

        private static void PrintStorageSorted(Storage storage,
                                               Sorter.Compare<Product> compare,
                                               string by)
        {
            Product[] products = storage.ToArray();
            Sorter.Sort(products, compare);
            Console.WriteLine($"Products, sorted by {by}:");
            foreach (Product product in products)
                Console.WriteLine(product);
            Console.WriteLine();
        }

        private static void CheckDemo(Storage storage)
        {
            Check check = GenerateRandomCheck(storage);

            if (check is not null)
            {
                Console.WriteLine("Randomly generated check:");
                Console.WriteLine(check);
            }
            else
            {
                Console.WriteLine("Failed to generate a random check as the storage is empty");
            }
        }

        private static Check GenerateRandomCheck(Storage storage)
        {
            int storageProductsCount = storage.Count;

            if (storageProductsCount > 0)
            {
                var random = new Random();
                var buys = new Buy[10];
                for (var i = 0; i < 10; ++i)
                {
                    buys[i] = new Buy(storage[random.Next(storageProductsCount)],
                                      random.Next(1, 12));
                }

                return new Check(buys);
            }

            return null;
        }
    }
}
