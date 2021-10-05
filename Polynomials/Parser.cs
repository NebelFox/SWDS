using System;
using System.Collections.Generic;

namespace Polynomials
{
/*
    public static class Parser
    {
        public static Polynomial Parse(string s)
        {
            var monoms = new LinkedList<Monom>();
            var i = 0;

            for (; i < s.Length; ++i)
            {
                var j = 0;
                uint power = 0;
                double multiplier = s[i] == '-' ? -1f : 1f;
                ++i;
                while (char.IsDigit(s[i]) || s[i] == '.')
                    ++j;
                if (j > 0)
                {
                    if (!double.TryParse(s[i..(i + j)], out double value))
                        throw new FormatException();
                    multiplier *= value;
                }

                i += j;
                if (s[i] == 'x')
                {
                    power = 1;
                    j = 0;
                    if (s[++i] == '^')
                    {
                        ++i;
                        while (char.IsDigit(s[i + j]))
                            ++j;
                        if (j > 0)
                        {
                            if (!int.TryParse(s[i..(i + j)], out int value))
                            {
                                throw new FormatException();
                            }

                            power = (uint)value;
                            i += j;
                        }
                        else
                        {
                            throw new FormatException("No integer after power specifier '^'");
                        }
                    }
                }

                monoms.AddLast(new Monom(multiplier, power));
            }

            return new Polynomial(monoms);
        }

        public static string Prepare(string s)
        {
            string result = s.EraseWhitespaces();
            switch (result[0])
            {
            case '+':
            case '-':
                return result;
            default:
                return $"+{result}";
            }
        }
    }
*/
}
