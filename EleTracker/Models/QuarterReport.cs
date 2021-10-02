using System;
using System.Collections.Generic;
using System.Linq;
using EleTracker.Properties;

namespace EleTracker.Models
{
    using Usage = Tuple<int, int>;
    
    public class QuarterReport
    {
        private const int MonthsInQuarter = 3;
        
        public Quarter Quarter { get; }
        private readonly (int, string)[] _flatsInfo;
        private readonly Usage[,] _usages;
        private int _recordsCount;
        private readonly int _recordsCapacity;

        public QuarterReport(Quarter quarter, int recordsCapacity)
        {
            Quarter = quarter;
            _recordsCapacity = recordsCapacity;
            _recordsCount = 0;
            _flatsInfo = new (int, string)[recordsCapacity];
            _usages = new Usage[recordsCapacity, MonthsInQuarter];
        }

        public void AddRecord(int flatNumber, string ownerName, int[] usages)
        {
            if (_recordsCount >= _recordsCapacity)
                throw new InvalidOperationException("The report contains max amount of records");

            if (usages.Length != MonthsInQuarter * 2)
                throw new ArgumentException($"Must contain exactly {MonthsInQuarter * 2} values");

            _flatsInfo[_recordsCount] = (flatNumber, ownerName);
            for(var i=0; i<MonthsInQuarter; ++i)
                _usages[_recordsCount, i] = new Usage(usages[i * 2], usages[i * 2 + 1]);
            ++_recordsCount;
        }

        public void PrintFull(Action<string> print)
        {
            for(var i=0; i<_recordsCount; ++i)
                print($"{i + 1}. {PrettifyRecord(i)}\n");
        }

        public void PrintForFlat(int flatNumber, Action<string> print)
        {
            var index = 0;
            while (index < _recordsCapacity && _flatsInfo[index].Item1 != flatNumber)
                ++index;
            
            if (index == _recordsCapacity)
                throw new ArgumentException($"No records for flat #{flatNumber}");

            print($"{PrettifyRecord(index)}\n");
        }
        
        private string PrettifyRecord(int index)
        {
            (int flatNumber, string ownerName) = _flatsInfo[index];
            return $"Usage of flat #{flatNumber} owned by {ownerName}: {PrettifyUsage(index)}";
        }

        private string PrettifyUsage(int index)
        {
            return string.Join(", ", 
                               Enumerable.Range(0, MonthsInQuarter)
                                         .Select(i => $"{_usages[index,i].Item1} - {_usages[index,i].Item2} for {GetMonthByIndex(i)}"));
        }

        private Months GetMonthByIndex(int index)
        {
            if (index is >= MonthsInQuarter or < 0)
                throw new ArgumentException("Must be within [0, 2]", nameof(index));

            return (Months)((int)Quarter * MonthsInQuarter + index);
        }

        public string GetOwnerOfFlatWithHighestUsage()
        {
            int maxUsage = -1;
            int maxUsageIndex = -1;
            for (var i = 0; i < _recordsCapacity; ++i)
            {
                int usage = GetTotalUsage(i);
                if (usage > maxUsage)
                {
                    maxUsage = usage;
                    maxUsageIndex = i;
                }
            }

            return _flatsInfo[maxUsageIndex].Item2;
        }

        public int[] FlatsWithNoUsage()
        {
            var numbers = new LinkedList<int>();
            for (var i = 0; i < _recordsCapacity; ++i)
            {
                if (GetTotalUsage(i) == 0)
                    numbers.AddLast(_flatsInfo[i].Item1);
            }

            return numbers.ToArray();
        }

        private int GetTotalUsage(int index)
        {
            if (index is >= MonthsInQuarter or < 0)
                throw new ArgumentException("Must be within [0, 2]", nameof(index));
            
            return Enumerable.Range(0, MonthsInQuarter)
                             .Sum(i => _usages[index, i].Item1 - _usages[index, i].Item2);
        }
    }
}