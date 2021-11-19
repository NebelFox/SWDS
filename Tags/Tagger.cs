using System;
using System.Linq;

namespace Tags
{
    public static class Tagger
    {
        public static string Tag(string source)
        {
            string[] split = source.Split('#');

            if (split.Length == 1)
                return source;

            int half = split.Length >> 1; // split.Length / 2;
            return @$"{string.Join('<', split[..half])}<{
                split[half]}>{string.Join('>', split[(half + 1)..])}";
        }

        public static string[] Tag(string[] source)
        {
            if (source.Length == 1)
                return new[] { Tag(source[0]) };

            string[][] split = source.Select(s => s.Split('#').ToArray()).ToArray();
            int count = split.Select(s => s.Length - 1).Sum();
            var middle = 0;
            var k = split[middle].Length - 1;
            while (k < count >> 1)
                k += split[++middle].Length - 1;

            return split[..middle]
                  .Select(s => string.Join('<', s))
                  .Append(BuildMiddle(split[middle], count, k))
                  .Concat(split[(middle + 1)..].Select(s => string.Join('>', s)))
                  .ToArray();
        }

        private static string BuildMiddle(string[] split, int count, int k)
        {
            if (split.Length == 1)
                return split[0];

            int leftCount = (count >> 1) - k + split.Length - 1; 
            Console.WriteLine($"{count >> 1} - {k} + {split.Length} - 1 = {leftCount}");

            return string.Join(leftCount > (split.Length-1) >> 1 ? '<' : '>',
                               string.Join('<', split[..(leftCount+1)]),
                               string.Join('>', split[(leftCount+1)..]));
        }

        /*private static string[] Tag2(string[] lines)
        {
            int[] counts = lines.Select(l => l.Count(c => c is '#')).ToArray();
            int half = counts.Sum() >> 1;
            int middle = 0;
            int k = counts[0];
            while (k < half)
                k += counts[++middle];
            return lines[..middle].Select(l => l.Replace('#', '<'))
                                  .Append()
        }*/

        /*private static string BuildMiddle2(string line, int half, int count, int k)
        {
            int leftCount = half - k + count;
            return line.Replace('#', '<', )
        }*/
    }
}
