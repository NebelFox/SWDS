using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Interactors;
using Shop.Models;
using Task.Data;

namespace Task.Interactors
{
    public static class ProductCreationPrompter
    {
        private static readonly IReadOnlyList<string> InputVariants;

        static ProductCreationPrompter()
        {
            InputVariants = new[]
            {
                "- dairy <name> <price> <weight> <days-to-expire>",
                "- meat <category> <kind> <name> <price> <weight>"
            };
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Welcome! Use one of the following input variants: ");
            Console.WriteLine(string.Join('\n', InputVariants));
            Console.Write("  ? <category>: ");
            Console.WriteLine(string.Join(" | ",
                                          Enum.GetValues<Category>()
                                              .Select(EnumValueToString)));
            Console.Write("  ? <kind>: ");
            Console.WriteLine(string.Join(" | ",
                                          Enum.GetValues<MeatKind>()
                                              .Select(EnumValueToString)));
            Console.WriteLine("- help - show this message again");
            Console.WriteLine("- exit - complete the prompt");
        }

        private static string EnumValueToString<TEnum>(TEnum value) where TEnum : Enum
        {
            return value.ToString().ToLower().Replace('_', '-');
        }

        public static IEnumerable<Product> StartDialog()
        {
            ShowHelp();
            Product product = Prompt();
            while (product != null)
            {
                yield return product;
                product = Prompt();
            }
        }

        public static Product Prompt()
        {
            string input = Console.ReadLine();
            while (input == null)
            {
                Console.WriteLine("Empty input. Try again");
                input = Console.ReadLine();
            }

            switch (input)
            {
            case "exit":
                return null;
            case "help":
                ShowHelp();
                return Prompt();
            default:
                return ProductParser.Parse(input);
            }
        }
    }
}
