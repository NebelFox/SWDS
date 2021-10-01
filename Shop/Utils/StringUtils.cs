using System;
using System.Linq;

namespace Task.Views
{
    public static class StringUtils
    {
        public static string ToTitle(this string text)
        {
            return $"{text[..1].ToUpper()}{text[1..].ToLower()}";
        }

        public static string SnakeToPascalSpaced(this string text)
        {
            return string.Join(' ',
                               text.Split('_')
                                   .Select(s => s.ToTitle()));
        }

        public static string ToTitle<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return enumValue.ToString().SnakeToPascalSpaced();
        }
    }
}
