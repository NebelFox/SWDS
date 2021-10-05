using System;

namespace Polynomials
{
    public static class Utils
    {
        public static string EraseWhitespaces(this string s)
        {
            return string.Join("", s.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
