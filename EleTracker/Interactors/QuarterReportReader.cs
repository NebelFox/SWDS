using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EleTracker.Models;
using EleTracker.Properties;

namespace EleTracker.Interactors
{
    public static class QuarterReportReader
    {
        public static QuarterReport ReadFromFile(string filepath)
        {
            using var reader = new StreamReader(filepath);
            if(!Enum.TryParse(reader.ReadLine().Trim(), out Quarter quarter))
                throw new FormatException($"First line must define report quarter (valid values: {string.Join('|', Enum.GetNames<Quarter>())})");
            if (!int.TryParse(reader.ReadLine().Trim(), out int count))
                throw new FormatException("Second line must be a number of report records");

            var report = new QuarterReport(quarter, count);
            for (var i = 0; i < count; ++i)
            {
                if (reader.EndOfStream)
                    throw new FormatException(
                        "File contains less records than is declared in the second line");
                ParseRecordToReport(reader.ReadLine(), report);
            }

            if (!reader.EndOfStream)
                throw new FormatException(
                    "File contains more records than is declared in the second line");
            return report;
        }

        private static void ParseRecordToReport(string record, QuarterReport report)
        {
            var recordRegex = new Regex(@"^\s*(?<flatNumber>\d+)\s+(?<ownerName>\S+)(?:\s+(?<usage>\d+)){6}$");
            Match match = recordRegex.Match(record);
            if (!match.Success)
                throw new ArgumentException("Invalid format", nameof(record));

            var flatNumber = int.Parse(match.Groups["flatNumber"].Value);
            string ownerName = match.Groups["ownerName"].Value;
            int[] usages = match.Groups["usage"].Captures
                                .Select(capture => int.Parse(capture.Value))
                                .ToArray();
            report.AddRecord(flatNumber, ownerName, usages);
        }
    }
}
