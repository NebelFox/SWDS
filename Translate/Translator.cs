using System.Collections.Generic;

namespace Translate
{
    public class Translator
    {
        private readonly Translation _translation;

        public Translator(IReadOnlyDictionary<string, string> initialMapping)
        {
            _translation = new Translation(initialMapping);
        }

        public Translator()
        {
            _translation = new Translation();
        }

        public string Translate(string text)
        {
            (IList<string> values, IEnumerable<int> wordIndices) = Tokenizer.Tokenize(text);
            foreach (int index in wordIndices)
            {
                values[index] = _translation[values[index]];
            }
            return string.Join(string.Empty, values);
        }
    }
}
