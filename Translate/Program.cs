using System;
using System.Collections.Generic;
using System.IO;

namespace Translate
{
    class Program
    {
        private static void Main(string[] args)
        {
            var translator = new Translator(GetInitialMapping());
            
            string text = GetTextToTranslate();
            string translated = translator.Translate(text);
            Console.WriteLine(@$"Original Text:{text}\n\nTranslated Text:\n{translated}");
        }

        private static string GetTextToTranslate()
        {
            return string.Join('\n', GetLinesFromConsoleOrFile());
        }

        private static IReadOnlyDictionary<string, string> GetInitialMapping()
        {
            Console.WriteLine(@"You are going to set the initial mapping for translator.
Mapping format: one pair per line, pair items separated by a space");
            
            var mapping = new Dictionary<string, string>();
            
            foreach (string line in GetLinesFromConsoleOrFile())
            {
                string[] pair = line.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                
                if (pair.Length != 2)
                    Console.WriteLine($"Skipping line \"{line}\" as its items count != 2");
                else if(mapping.ContainsKey(pair[0]))
                    Console.WriteLine($"Skipping line \"{line}\" as its first item is already in the mapping");
                else
                    mapping[pair[0]] = pair[1];
            }
            return mapping;
        }

        private static IEnumerable<string> GetLinesFromConsoleOrFile()
        {
            Console.WriteLine("Enter name of the file to read from\nOr leave blank to enter data via console");
            string filename = Console.ReadLine();

            return string.IsNullOrWhiteSpace(filename)
                ? GetLinesFromConsole()
                : GetLinesFromFile(filename);
        }

        private static IEnumerable<string> GetLinesFromConsole()
        {
            Console.WriteLine("You may enter as many lines as you want.\nTo finish - enter a blank line.");
            string line = Console.ReadLine();
            while (string.IsNullOrEmpty(line) == false)
            {
                yield return line;
                line = Console.ReadLine();
            }
        }

        private static IEnumerable<string> GetLinesFromFile(string filename)
        {

            if (Directory.Exists(filename) == false)
                throw new FileNotFoundException("File not found", filename);
            
            using var reader = new StreamReader(filename);
            while (reader.EndOfStream == false)
                yield return reader.ReadLine();
        }
    }
}
