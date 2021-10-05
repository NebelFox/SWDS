using System;
using System.Collections.Generic;
using System.ComponentModel;
using Shop.Models;
using Shop.Models.Products;
using Shop.Utils;
using Task.Data;

namespace Shop.Interactors
{
    public static class ProductCreator
    {
        private static readonly Dictionary<ProductKind,
            Func<string[], Product>> ProductFactories;

        static ProductCreator()
        {
            ProductFactories
                = new Dictionary<ProductKind, Func<string[], Product>>();
            RegisterFactory(ProductKind.Product,
                            options =>
                            {
                                (string name, 
                                 float price, 
                                 double weight, 
                                 DateTime dateCreated, 
                                 uint daysToExpire) = ParseProductOptions(options);

                                return new Product(name, price, weight, dateCreated, daysToExpire);
                            });
            RegisterFactory(ProductKind.Meat,
                            options =>
                            {
                                // category kind name price weight
                                Enum.TryParse(options[0],
                                              true,
                                              out Category category);
                                Enum.TryParse(options[1],
                                              true,
                                              out MeatKind meatKind);
                                (string name, 
                                 float price, 
                                 double weight, 
                                 DateTime dateCreated, 
                                 uint daysToExpire) = ParseProductOptions(options[2..]);

                                return new Meat(name,
                                                price,
                                                weight,
                                                dateCreated,
                                                daysToExpire,
                                                category,
                                                meatKind);
                            });
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

        private static (string, float, double, DateTime, uint) ParseProductOptions(IReadOnlyList<string> options)
        {
            if (!DateTime.TryParse(options[3], out DateTime dateCreated))
                throw new ArgumentException($"Invalid DateTime format: {options[3]}");

            return (options[0],
                    NumUtils.ParseFloat(options[1]),
                    NumUtils.ParseDouble(options[2]),
                    dateCreated,
                    uint.Parse(options[4]));
        }
    }
}
