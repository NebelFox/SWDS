using System;
using static System.Int32;

namespace MagicSquare
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StartDialog();
        }

        private static void StartDialog()
        {
            Console.WriteLine("Input format: 'size [up-right|down-right]'");
            Console.WriteLine("'exit' to terminate the program");
            while (true)
            {
                Console.WriteLine("");
                string input = Console.ReadLine();
                if (input == "exit")
                    break;
                string[] tokens = input.Split();
                if (TryParse(tokens[0], out int size))
                {
                    if (tokens.Length > 1
                        && Enum.TryParse(tokens[1]
                                        .ToUpper()
                                        .Replace('-', '_'),
                                         out Strategy strategy))
                        Generate(size, strategy);
                    Console.WriteLine(
                        $"Please use any of {string.Join(", ", Enum.GetNames<Strategy>())}");
                }
                else
                {
                    Console.WriteLine("Please enter an integer");
                }

                Console.WriteLine();
            }
        }

        private static void Generate(int size, Strategy strategy)
        {
            int[,] square;
            try
            {
                square = Generator.Generate(size, strategy);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Utils.PrintMatrix(square);
            var validator = new Validator(square);
            Console.WriteLine($"Is the generated square valid: {validator.IsValid()}");
        }
    }
}
