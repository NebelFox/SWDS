using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Statistics
{
    public class Analyzer
    {
        private readonly IReadOnlyDictionary<string, LinkedList<Visit>> _visits;

        public Analyzer(IDictionary<string, LinkedList<Visit>> visits)
        {
            _visits = new ReadOnlyDictionary<string, LinkedList<Visit>>(visits);
        }

        public IDictionary<string, int> GetVisitsPerIp()
        {
            return new Dictionary<string, int>(
                _visits.Select(pair => new KeyValuePair<string, int>(pair.Key, pair.Value.Count)));
        }

        public IDictionary<string, int> GetMostVisitedDayPerIp()
        {
            return new Dictionary<string, int>(
                _visits.Select(pair => new KeyValuePair<string, int>(pair.Key, GetMostVisitedDayFor(pair.Value))));
        }

        private static int GetMostVisitedDayFor(IEnumerable<Visit> visits)
        {
            return visits.GroupBy(v => v.DayIndex).Max(g => g.Count());
        }

        public IDictionary<string, int> GetMostVisitedHourPerIp()
        {
            return new Dictionary<string, int>(
                _visits.Select(pair => new KeyValuePair<string, int>(pair.Key, GetMostVisitedDayFor(pair.Value))));
        }

        private static int GetMostVisitedHourFor(IEnumerable<Visit> visits)
        {
            return visits.GroupBy(v => v.Time.Hours).Max(g => g.Count());
        }

        public int GetMostVisitedHour()
        {
            return _visits.SelectMany(pair => pair.Value).GroupBy(v => v.Time.Hours).Max(g => g.Count());
        }
    }
}
