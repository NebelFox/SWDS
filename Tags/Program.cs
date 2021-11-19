using System;

namespace Tags
{
    class Program
    {
        private static string ExtractFileName(string filepath)
        {
            int dashIndex = filepath.LastIndexOf('\\');
            int dotIndex = filepath.LastIndexOf('.');
            return dotIndex > dashIndex ? filepath[(dashIndex + 1)..dotIndex] : filepath[(dashIndex + 1)..];
        }

        private static string ExtractParentDirectory(string filepath)
        {
            int nameDashIndex = filepath.LastIndexOf('\\');
            int parentStart = filepath.LastIndexOf('\\', nameDashIndex) + 1;
            return filepath[parentStart..nameDashIndex];
        }

        static void Main(string[] args)
        {
            Console.WriteLine(string.Join('\n',
                                          Tagger.Tag(new[]
                                          {
                                              "#Is it possible to #obtain another",
                                              "result by canceling the condition",
                                              "about the# nesting#"
                                          })));
        }
    }
}
