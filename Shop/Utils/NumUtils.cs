using System.Globalization;

namespace Shop.Utils
{
    public static class NumUtils
    {
        public static float PercentsToFloat(int percents)
        {
            return 100f / percents;
        }

        public static float ExtraPercentsToFloat(int percents)
        {
            return 1 + PercentsToFloat(percents);
        }

        public static float ParseFloat(string s)
        {
            return float.Parse(s, NumberStyles.Number, CultureInfo.InvariantCulture);
        }

        public static double ParseDouble(string s)
        {
            return double.Parse(s, NumberStyles.Number, CultureInfo.InvariantCulture);
        }
    }
}
