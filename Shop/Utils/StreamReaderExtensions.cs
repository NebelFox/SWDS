using System.Collections.Generic;
using System.IO;

namespace Shop.Utils
{
    public static class StreamReaderExtensions
    {
        public static IEnumerable<string> ReadLines(this StreamReader reader)
        {
            while (reader.EndOfStream == false)
                yield return reader.ReadLine();
        }
    }
}
