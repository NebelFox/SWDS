using System;
using System.Collections.Generic;
using Dishes.Controllers;
using Dishes.Models;

namespace Dishes
{
    class Program
    {
        private static void Main(string[] args)
        {
            string productsFilepath = Ask("Enter path to file with products: ");
            IEnumerable<Product> products = ProductsLoader.LoadFromFile(productsFilepath);
            
            string dishesFilepath = Ask("Enter path to file with dishes: ");
            IEnumerable<Dish> dishes = DishesLoader.LoadFromFile(dishesFilepath);

            IReadOnlyDictionary<string, double> quantities = QuantitySummarizer.Summarize(dishes);
            IEnumerable<Need> needs = NeedsSummarizer.SummarizeNeeds(products, quantities);
        }

        private static void ViewNeeds(IEnumerable<Need> needs)
        {
            Console.WriteLine("Total needs list:");
            foreach (Need need in needs)
                Console.WriteLine($"{need.Quantity} kg of {need.Name}, costing {need.Price}");
        }

        private static string Ask(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }
    }
}
