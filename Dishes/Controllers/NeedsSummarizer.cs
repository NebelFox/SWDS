using System.Collections.Generic;
using System.Linq;
using Dishes.Models;

namespace Dishes.Controllers
{
    public static class NeedsSummarizer
    {
        // name, quantity, price
        public static IEnumerable<Need> SummarizeNeeds(IEnumerable<Product> products,
                                                  IReadOnlyDictionary<string, double> quantities)
        {
            IReadOnlyDictionary<string, double> prices = GetPrices(products);
            var needs = new List<Need>();
            foreach ((string name, double quantity) in quantities)
            {
                if (prices.ContainsKey(name) == false)
                    throw new KeyNotFoundException($"No product '{name}'");
                needs.Add(new Need(name, quantity, prices[name] * quantity));
            }
            return needs;
        }

        private static IReadOnlyDictionary<string, double> GetPrices(IEnumerable<Product> products)
        {
            return new Dictionary<string, double>(
                products.Select(p => new KeyValuePair<string, double>(p.Name, p.Price)));
        }
    }
}
