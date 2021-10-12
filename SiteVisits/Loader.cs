using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Statistics
{
    public class Loader
    {
        private const int ExpectedTokensCount = 3;
        private static readonly List<string> DayNames = new(DateTimeFormatInfo.InvariantInfo.DayNames);
        private readonly string _filepath;

        public Loader(string filepath)
        {
            if (filepath is null)
                throw new ArgumentNullException(nameof(filepath));
            if (!Directory.Exists(filepath))
                throw new FileNotFoundException("File not found", filepath);
            _filepath = filepath;
        }

        public IDictionary<string, LinkedList<Visit>> Load()
        {
            using var reader = new StreamReader(_filepath);
            string[] lines = reader.ReadToEnd()
                                   .Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var visits = new Dictionary<string, LinkedList<Visit>>(lines.Length);
            for (var i = 0; i < lines.Length; ++i)
            {
                string[] tokens = lines[i].Split();
                if (tokens.Length != ExpectedTokensCount)
                    throw MakeFormatExceptionInLine(i+1, @$" got {
                        tokens.Length} tokens, while expected {ExpectedTokensCount}");
                
                if (!visits.ContainsKey(tokens[0]))
                    visits[tokens[0]] = new LinkedList<Visit>();
                Visit visit = LoadVisit(tokens, i+1);
                visits[tokens[0]].AddLast(visit);
            }
            return visits;
        }

        private Visit LoadVisit(string[] tokens, int lineIndex)
        {
            if (!TimeSpan.TryParse(tokens[1], out TimeSpan time))
                throw MakeFormatExceptionInLine(lineIndex, $"invalid time format ({tokens[1]})");
            
            if (!TryParseDayName(tokens[2], out int index))
                throw MakeFormatExceptionInLine(lineIndex, $"invalid day name ({tokens[2]})");
            
            return new Visit(time, index);
        }

        private static FormatException MakeFormatExceptionInLine(int lineIndex, string message)
        {
            return new FormatException($"Line {lineIndex}: {message}.");
        }

        private static bool TryParseDayName(string name, out int index)
        {
            index = DayNames.FindIndex(value => value == name);
            return index != -1;
        }
    }
}
