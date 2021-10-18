using System;
using System.Collections.Generic;
using System.IO;
using Dishes.Models;
using Dishes.Utils;

namespace Dishes.Controllers
{
    public static class DishesLoader
    {
        public static IEnumerable<Dish> LoadFromFile(string filepath)
        {
            FilepathUtils.ValidateFilepath(filepath);
            
            using var reader = new StreamReader(filepath);
            var lineIndex = 0;
            
            while (!reader.EndOfStream)
            {
                string name = reader.ReadLine();
                ++lineIndex;
                
                var ingredients = new List<Ingredient>();
                
                string line = reader.ReadLine();
                ++lineIndex;
                while (!reader.EndOfStream && !string.IsNullOrWhiteSpace(line))
                {
                    ingredients.Add(ParseIngredient(line, lineIndex));
                    line = reader.ReadLine();
                    ++lineIndex;
                }
                
                if (ingredients.Count == 0)
                    throw new FormatException($"Dish '{name}' has no ingredients");
                
                yield return new Dish(name, ingredients);
            }
        }

        private static Ingredient ParseIngredient(string line, int lineIndex)
        {
            string[] tokens = line.Split();

            if (string.IsNullOrWhiteSpace(tokens[0]))
                throw new FormatException($"Line {lineIndex}: ingredient name can't be empty");

            if (!double.TryParse(tokens[1], out double quantity))
                throw new FormatException(
                    $"Line {lineIndex}: expected double as ingredient quantity, got '{quantity}'");

            return new Ingredient(tokens[0], quantity);
        }
    }
}
