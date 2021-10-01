using System;
using Task.Interactors;
using Task.Models;

namespace Task
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var storage = new Storage();
            storage.Add(ProductCreationPrompter.StartDialog());

            storage.View();

            int storageProductsCount = storage.Count;

            if (storageProductsCount > 0)
            {
                var random = new Random();
                var buys = new Buy[10];
                for (var i = 0; i < 10; ++i)
                    buys[i] = new Buy(storage[random.Next(storageProductsCount)],
                                      random.Next(1, 12));

                var check = new Check(buys);
                Console.WriteLine("Randomly generated check:");
                check.View();
            }
            else
            {
                Console.WriteLine("Failed to generate a random check as the storage is empty");
            }
        }
    }
}
