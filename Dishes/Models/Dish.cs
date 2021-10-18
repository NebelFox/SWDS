using System.Collections.Generic;

namespace Dishes.Models
{
    public record Dish(string Name, IEnumerable<Ingredient> Ingredients);
}
