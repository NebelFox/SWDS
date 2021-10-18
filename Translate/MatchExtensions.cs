using System.Text.RegularExpressions;

namespace Translate
{
    public static class MatchExtensions
    {
        public static int End(this Match match)
        {
            return match.Index + match.Length;
        }
    }
}
