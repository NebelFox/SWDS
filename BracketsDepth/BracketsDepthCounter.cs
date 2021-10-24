using System;
using System.Collections.Generic;
using System.Linq;

namespace BracketsDepth
{
    public static class BracketsDepthCounter
    {
        public static int GetMaxDepth(IEnumerable<string> lines, char opening, char closing)
        {
            return lines.Max(l => GetMaxDepth(l, opening, closing));
        }

        private static int GetMaxDepth(string line, char opening, char closing)
        {
            var depth = 0;
            var maxDepth = 0;
            foreach (char c in line)
            {
                depth += c == opening
                    ? 1
                    : c == closing
                        ? -1
                        : 0;
                maxDepth = Math.Max(depth, maxDepth);
            }
            return maxDepth;
        }
    }
}
