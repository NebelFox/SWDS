using System;
using System.Linq;

namespace Shadows
{
    class Program
    {
        static void Main(string[] args)
        {
            var renderer = new Renderer(' ', 'x', '-');
            // 5x5x5
            // '*' - true
            // ' ' - false
            var origin = new string[,]
            {
                { "     ", " * * ", "     ", " * * ", "     " },
                { "     ", " * * ", "     ", " * * ", "     " },
                { "     ", " * * ", "  *  ", " * * ", "     " },
                { "     ", " * * ", "     ", " * * ", "     " },
                { "     ", " * * ", "     ", " * * ", "     " }
            };
            bool[,,] shape = Shaper.FromStrings(origin, c => c == '*');

            for (var i = 0; i < 5; ++i)
                Console.WriteLine($"| {string.Join(" | ", Enumerable.Range(0, 5).Select(j => origin[j, i]))} |");

            foreach (Prospect prospect in Enum.GetValues<Prospect>())
            {
                Console.WriteLine($"Prospect: {prospect}");
                bool[,] shade = Shader.Shade(shape, prospect);
                Console.WriteLine(renderer.Render(shade));
                Console.WriteLine();
            }
        }
    }
}
