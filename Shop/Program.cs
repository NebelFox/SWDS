using System;
using System.IO;
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
            
        }

        private static void Demo()
        {
            Storage first = CreateStorageFromFileWithLoggingInvalidLines();
            first.View();
            
            Storage second = CreateStorageFromFileWithPromptingToReplaceInvalidLines();
            Console.WriteLine();
            second.View();
        }

        private static Storage CreateStorageFromFileWithLoggingInvalidLines()
        {
            Console.WriteLine("Enter input file: ");
            string inputFilepath = Console.ReadLine();
            Console.WriteLine("Enter file to report invalid lines to: ");
            string logFilepath = Console.ReadLine();
            using var writer = new StreamWriter(logFilepath);
            return CreateStorageFromFile(inputFilepath,
                                         (_, lineIndex, exception) =>
                                         {
                                             writer.WriteLine(
                                                 $"{DateTime.Now} | line {lineIndex} | {exception.Message}");
                                         });
        }

        private static Storage CreateStorageFromFileWithPromptingToReplaceInvalidLines()
        {
            Console.WriteLine("Enter input file: ");
            string inputFilepath = Console.ReadLine();
            return CreateStorageFromFile(inputFilepath,
                                         (storage, lineIndex, exception) =>
                                         {
                                             Console.WriteLine($"Error at line {lineIndex}: {exception.Message}");
                                             Console.WriteLine("Would you like to enter a product manually? (yes/no)");
                                             string response = Console.ReadLine();
                                             if (response == "yes")
                                                 storage.Add(ProductCreationPrompter.Prompt());
                                         });
        }

        private static void StorageMatcherDemo()
        {
            Storage lhs = CreateStorageViaConsole();
            Storage rhs = CreateStorageViaConsole();

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
            Storage storage = CreateStorageViaConsole();

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

        private static Storage CreateStorageFromFile(string filepath,
                                                     Action<Storage, int, Exception> onLineFailed)
        {
            var today = DateTime.Parse(Console.ReadLine());
            var deserializer = new ProductsDeserializer();
            var storage = new Storage(today);
            deserializer.LineFailed += (lineIndex, exception) => onLineFailed(storage, lineIndex, exception);
            storage.Add(deserializer.DeserializeFile(filepath));
            return storage;
        }

        private static Storage CreateStorageViaConsole()
        {
            Console.WriteLine("Enter a file to write reports about expired products to:");
            string filepath = Console.ReadLine();
            var reporter = new ExpiresReporter(filepath);

            Console.WriteLine("Enter the starting date for the storage:");
            var today = DateTime.Parse(Console.ReadLine());

            var storage = new Storage(today);
            storage.Add(ProductCreationPrompter.StartDialog());
            storage.ProductExpired += product =>
            {
                storage.Remove(product);
                reporter.Report(storage.Today, product);
            };

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
