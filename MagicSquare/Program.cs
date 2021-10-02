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
            const Strategy defaultStrategy = Strategy.DownRight;
            Console.WriteLine("Input format: 'size strategy'");
            Console.WriteLine($"strategy: {string.Join('|', Enum.GetNames<Strategy>())}, {defaultStrategy.ToString()} by default");
            Console.WriteLine("'exit' to terminate the program");
            while (true)
            {
                Console.WriteLine("");
                string input = Console.ReadLine();
                
                if (input == "exit")
                {
                    Console.WriteLine("Terminating the program");
                    break;
                }
                
                string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (TryParse(tokens[0], out int size))
                {
                    if (tokens.Length > 1)
                    {
                        if(Enum.TryParse(tokens[1], out Strategy strategy))
                            Generate(size, strategy);
                        else
                        {
                            Console.WriteLine(
                                $"Invalid strategy. Use any of {{{string.Join(", ", Enum.GetNames<Strategy>())}}}");
                        }
                    }
                    else
                    {
                        Generate(size, defaultStrategy);
                    }
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
