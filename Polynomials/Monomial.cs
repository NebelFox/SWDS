using System;
using System.Globalization;
using System.Text;

namespace Polynomials
{
    public record Monomial(double Multiplier, uint Power)
    {
        public override string ToString()
        {
            var builder = new StringBuilder();
            if (Multiplier < 0f)
                builder.Append('-');
            if (Power == 0 || Multiplier != 1f && Multiplier != -1f)
                builder.Append(Multiplier > 0 ? Multiplier : -Multiplier);
            if (Power != 0)
            {
                builder.Append('x');
                if(Power != 1)
                {
                    builder.Append('^').Append(Power);
                }
            }

            return builder.ToString();
        }

        public string ToStringUnsigned()
        {
            double m = Math.Abs(Multiplier);
            string multiplier = m != 1f 
                                    ? m.ToString(CultureInfo.InvariantCulture) 
                                    : string.Empty;
            string power = Power != 0
                               ? Power != 1
                                     ? $"x^{Power}"
                                     : "x"
                               : string.Empty;
            return $"{multiplier}{power}";
        }
    }
}
