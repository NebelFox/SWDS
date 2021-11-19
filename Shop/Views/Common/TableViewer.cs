using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Views.Common
{
    public delegate object Selector<in TItem>(TItem item);

    public class TableViewer<TItem>
    {
        private record Column(string Name, Selector<TItem> Selector);
        
        private IReadOnlyList<TItem> _items;
        private readonly Func<IReadOnlyList<TItem>> _fetch;
        private readonly IEnumerable<Column> _columns;
        private readonly string _separator;
        private readonly string _nullPlaceholder;
        private readonly string _indexTitle;

        public TableViewer(Func<IReadOnlyList<TItem>> fetch,
                         IEnumerable<(string columnName, Selector<TItem> selector)> columns,
                         string separator = " | ",
                         string nullPlaceholder = "--",
                         string indexTitle = "Index")
        {
            _fetch = fetch;
            _columns = columns.Select(c => new Column(c.columnName, c.selector));
            _separator = separator ?? throw new ArgumentNullException(nameof(separator));
            _nullPlaceholder = nullPlaceholder ?? throw new ArgumentNullException(nameof(nullPlaceholder));
            _indexTitle = indexTitle ?? throw new ArgumentNullException(nameof(indexTitle));
            Update();
        }

        public int Count => _items.Count;

        public TItem this[int index] => _items[index];

        public void Update()
        {
            _items = _fetch.Invoke();
        }

        public IEnumerable<string> View(int count = -1,
                                        int start = 0)
        {
            if (count == -1)
                count = _items.Count;

            ValidateArguments(count, start);

            var columns = new List<List<object>>(_items.Count + 1)
            {
                Enumerable.Range(start, count)
                          .Select(i => (object)i)
                          .Prepend(_indexTitle)
                          .ToList(),
            };
            columns.AddRange(_columns.Select(column => SelectColumn(column, start, count)));
            var widths = new List<int>(columns.Select(GetMaxWidth));
            for (var i = 0; i < count + 1; ++i)
            {
                int j = i;
                yield return string.Join(_separator,
                                         Justify(columns.Select(c => c[j]), widths));
            }
        }

        private void ValidateArguments(int count, int start)
        {
            if (start < 0 || start >= _items.Count && _items.Count > 0)
                throw new ArgumentOutOfRangeException(nameof(start), start, "is out of content bounds");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "count is negative");
            if (start + count > _items.Count)
                throw new ArgumentException("start+count is out of content bounds");
        }

        private List<object> SelectColumn(Column column, int start, int count)
        {
            Selector<TItem> selector = column.Selector;
            return Enumerable.Range(start, count)
                             .Select(i => selector(_items[i]))
                             .Prepend(column.Name)
                             .ToList();
        }

        private int GetMaxWidth(IEnumerable<object> objects)
        {
            int nullWidth = _nullPlaceholder.Length;
            return objects.Max(o => o != null ? o.ToString().Length : nullWidth);
        }

        private IEnumerable<string> Justify(IEnumerable<object> row,
                                            IReadOnlyList<int> widths)
        {
            return row.Select((o, i) => Pad(o, widths[i]));
        }

        private string Pad(object o, int width)
        {
            return (o ?? _nullPlaceholder).ToString().PadRight(width);
        }
    }
}
