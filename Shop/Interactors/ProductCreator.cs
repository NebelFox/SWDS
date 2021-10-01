using System;
using System.Collections.Generic;
using System.ComponentModel;
using Task.Data;
using Task.Models;
using Task.Utils;

namespace Task.Interactors
{
    public static class ProductCreator
    {
        private static readonly Dictionary<ProductKind,
            Func<string[], Product>> ProductFactories;

        static ProductCreator()
        {
            ProductFactories
                = new Dictionary<ProductKind, Func<string[], Product>>();
            RegisterFactory(ProductKind.MEAT,
                            options =>
                            {
                                // category kind name price weight
                                Enum.TryParse(options[0].Replace('-', '_'),
                                              true,
                                              out Category category);
                                Enum.TryParse(options[1].Replace('-', '_'),
                                              true,
                                              out MeatKind meatKind);
                                (string name, float price, double weight)
                                    = ParseProductOptions(options[2..]);
                                return new Meat(name,
                                                price,
                                                weight,
                                                category,
                                                meatKind);
                            });
            RegisterFactory(ProductKind.DAIRY,
                            options =>
                            {
                                // name, price, weight, days-to-expire
                                (string name, float price, double weight)
                                    = ParseProductOptions(options);
                                return new Dairy(name,
                                                 price,
                                                 weight,
                                                 int.Parse(options[3]));
                            });
            ;
        }

        public static void RegisterFactory(ProductKind productKind,
                                           Func<string[], Product> factory)
        {
            if (ProductFactories.ContainsKey(productKind))
                throw new InvalidEnumArgumentException(
                    $"{productKind.ToString()} factory is already registered");
            ProductFactories[productKind] = factory;
        }

        public static Product Create(string[] options)
        {
            if (!Enum.TryParse(options[0].Replace('-', '_'),
                               true,
                               out ProductKind productKind))
                throw new InvalidEnumArgumentException(
                    $"<product-kind> must be any of {string.Join(", ", Enum.GetValues<ProductKind>().ToString())}");

            return ProductFactories[productKind](options[1..]);
        }

        private static (string, float, double) ParseProductOptions(IReadOnlyList<string> options)
        {
            Console.WriteLine("Options: ");
            foreach (string option in options)
            {
                Console.Write(option);
                Console.Write(", ");
            }

            Console.Write('\n');
            return (options[0],
                    NumUtils.ParseFloat(options[1]),
                    NumUtils.ParseDouble(options[2]));
        }
    }
}
