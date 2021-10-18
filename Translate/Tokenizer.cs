using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Translate
{
    public static class Tokenizer
    {
        private static readonly Regex TokenRegex = new("\\w+");

        public static (IList<string>, IEnumerable<int>) Tokenize(string text)
        {
            MatchCollection matches = TokenRegex.Matches(text);
            if (matches.Count == 0)
                return (new[] { text }, Enumerable.Empty<int>());

            var values = new List<string>();
            var wordIndices = new List<int>();

            if (matches[0].Index != 0)
                values.Add(text[..matches[0].Index]);
                
            for (var i = 0; i < matches.Count-1; ++i)
            {
                values.Add(matches[i].Value);
                wordIndices.Add(values.Count-1);
                values.Add(text[matches[i].End()..matches[i + 1].Index]);
            }
            values.Add(matches[^1].Value);
            wordIndices.Add(values.Count-1);
                                
            if (matches[^1].End() < text.Length)
                values.Add(text[matches[^1].End()..]);
            
            return (values, wordIndices);
        }
    }
}
