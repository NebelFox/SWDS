using System;

namespace Polynomials
{
    class Program
    {
        private static void Main(string[] args)
        {
            Polynomial[] polynoms = {
                Polynomial.Parse("2x - x - 4x^2 + x^3 - 2"),
                Polynomial.Parse("2x - x - 4x^2 + x^3 - 2"), 
            };

            foreach (Polynomial polynomial in polynoms)
            {
                Console.WriteLine(polynomial);
            }

            Console.WriteLine($"+: {polynoms[0].Add(polynoms[1])}");
            Console.WriteLine($"-: {polynoms[0].Subtract(polynoms[1])}");
        }
    }
}
