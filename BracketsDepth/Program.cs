using System;
using System.IO;
using System.Linq;

namespace BracketsDepth
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter filepath for demo: ");
            string filepath = Console.ReadLine();
            DemoGetMaxBracketsDepth(filepath);
            DemoSortSentencesByLength(filepath);
        }

        private static void DemoGetMaxBracketsDepth(string filepath)
        {
            var reader = new StreamReader(filepath);
            int depth = BracketsDepthCounter.GetMaxDepth(reader.ReadLines('.'),
                                                         '(',
                                                         ')');
            Console.WriteLine($"Max brackets depth in {filepath} = {depth}");
        }

        private static void DemoSortSentencesByLength(string filepath)
        {
            var reader = new StreamReader(filepath);
            string[] sentences = reader.ReadLines('.').ToArray();
            Sorter.Sort(sentences,
                            (lhs, rhs) => lhs.Length.CompareTo(rhs.Length));
            Console.WriteLine("Sentences, sorted by length (in ascending order):");
            foreach (string sentence in sentences)
                Console.WriteLine(sentence);
        }
    }
}
