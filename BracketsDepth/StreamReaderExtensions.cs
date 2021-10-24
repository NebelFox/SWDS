using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BracketsDepth
{
    public static class StreamReaderExtensions
    {
        public static IEnumerable<string> ReadLines (this StreamReader reader, 
                                                     char delimiter)
        {
            var builder = new StringBuilder();
            while (reader.Peek() >= 0)
            {
                var c = (char)reader.Read ();

                if (c == delimiter) {
                    yield return builder.ToString();
                    builder.Clear();
                }
                else
                {
                    builder.Append(c);
                }
            }
        }
    }
}
