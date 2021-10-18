using System.Collections.Generic;
using Dishes.Models;

namespace Dishes.Controllers
{
    public static class QuantitySummarizer
    {
        public static IReadOnlyDictionary<string, double> Summarize(IEnumerable<Dish> dishes)
        {
            var quantities = new Dictionary<string, double>();
            foreach (Dish dish in dishes)
            {
                foreach (Ingredient ingredient in dish.Ingredients)
                {
                    if(quantities.ContainsKey(ingredient.Name) == false)
                        quantities.Add(ingredient.Name, 0f);
                    quantities[ingredient.Name] = ingredient.Quantity;
                }
            }
            return quantities;
        }
    }
}
