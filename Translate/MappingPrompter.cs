using System;

namespace Translate
{
    public static class MappingPrompter
    {
        private const string AgreeingAnswer = "y";

        public static string Prompt(string from)
        {
            Console.Write($"Enter translation for \"{from}\": ");
            string to = Console.ReadLine();
            if (string.IsNullOrEmpty(to))
            {
                Console.WriteLine("Input was empty");
                return Prompt(from);
            }
            
            if (to != from)
                return to;
            
            Console.Write("\"to\" is the same as \"from\". Are you sure? (y/n): ");
            string answer = Console.ReadLine();
            return answer == AgreeingAnswer ? to : Prompt(from);
        }
    }
}
