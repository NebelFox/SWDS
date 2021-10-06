using System;
using Shop.Interactors;
using Shop.Models;
using Task.Interactors;

namespace Shop
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter a file to write reports about expired products to:");
            string filepath = Console.ReadLine();
            var reporter = new ExpiresReporter(filepath);

            Console.WriteLine("Enter the starting date for the storage:");
            var today = DateTime.Parse(Console.ReadLine());

            var storage = new Storage(today, reporter);
            storage.Add(ProductCreationPrompter.StartDialog());

            storage.View();
            
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
