using System.Collections.Generic;

namespace Translate
{
    public class Translation
    {
        private readonly IDictionary<string, string> _mapping;

        public Translation(IReadOnlyDictionary<string, string> mapping)
        {
            _mapping = new Dictionary<string, string>(mapping);
        }

        public Translation() : this(new Dictionary<string, string>())
        { }

        /*public void Add(string from, string to)
        {
            if (!_mapping.TryAdd(from, to))
                throw new InvalidOperationException(
                    $"This instance already contains a pair with key \"{from}\"");
        }*/

        public string this[string from]
        {
            get
            {
                if (!_mapping.ContainsKey(from))
                    _mapping[from] = MappingPrompter.Prompt(from);
                return _mapping[from];
            }
        }
    }
}
