using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task.Views
{
    public static class StringUtils
    {
        public static string ToTitle(this string text)
        {
            return $"{text[..1].ToUpper()}{text[1..].ToLower()}";
        }

        public static string ScreamingSnakeToPascalSpaced(this string text)
        {
            return string.Join(' ',
                               text.Split('_')
                                   .Select(s => s.ToTitle()));
        }

        public static string ToTitle<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return enumValue.ToString().ScreamingSnakeToPascalSpaced();
        }
    }
}
