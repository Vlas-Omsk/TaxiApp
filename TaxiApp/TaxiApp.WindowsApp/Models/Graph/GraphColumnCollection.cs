using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TaxiApp.WindowsApp.Models.Graph
{
    public sealed class GraphColumnCollection : IEnumerable<IGraphColumn>
    {
        private IGraphColumn[] _items;

        public GraphColumnCollection(IEnumerable<IGraphColumn> items)
        {
            _items = items.ToArray();
        }

        public int Length => _items.Length;

        public IGraphColumn this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        public IEnumerator<IGraphColumn> GetEnumerator()
        {
            return ((IEnumerable<IGraphColumn>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Invert()
        {
            if (_items.Length == 0)
                return;

            ThrowIfNotDoubleDepth();

            var firstInnerColumns = _items[0] as GraphCollectionColumn;

            _items = firstInnerColumns.Columns
                .Select((x, i) =>
                {
                    return new GraphCollectionColumn(
                        x.Name,
                        _items
                            .Select((c, j) =>
                            {
                                var items = ((GraphCollectionColumn)c).Columns;
                                IGraphValue value = null;

                                if (i < items.Length)
                                    value = ((GraphValueColumn)items[i]).Value;

                                return new GraphValueColumn(c.Name, value);
                            })
                    );
                })
                .Cast<IGraphColumn>()
                .ToArray();
        }

        public void FindAverage()
        {
            if (_items.Length == 0)
                return;

            ThrowIfNotDoubleDepth();

            _items = _items
                .Select(x =>
                {
                    var items = ((GraphCollectionColumn)x).Columns;
                    var value = 0d;

                    if (items.Any())
                        value = items
                            .Cast<GraphValueColumn>()
                            .Where(x => x.Value != null)
                            .Average(x => x.Value.Value);

                    return new GraphValueColumn(x.Name, new GraphValue(value));
                })
                .Cast<IGraphColumn>()
                .ToArray();
        }

        public void ThrowIfNotDoubleDepth()
        {
            if (GetMaxDepth() != 2)
                throw new InvalidOperationException("Depth should be 2");

            foreach (var column in _items)
            {
                if (column is not GraphCollectionColumn innerColumns)
                    throw new InvalidOperationException($"All inner columns must be of type {nameof(GraphColumnCollection)}");

                foreach (var innerColumn in innerColumns.Columns)
                {
                    if (innerColumn is not GraphValueColumn)
                        throw new InvalidOperationException($"All inner columns must be of type {nameof(GraphValueColumn)}");
                }
            }
        }

        public int GetMaxDepth()
        {
            var depth = GetMaxDepthInternal(_items, 1);

            return depth;
        }

        public void AddRange(IEnumerable<IGraphColumn> columns)
        {
            _items = _items.Concat(columns).ToArray();
        }

        public void Add(IGraphColumn column)
        {
            _items = _items.Append(column).ToArray();
        }

        private int GetMaxDepthInternal(IEnumerable<IGraphColumn> columns, int baseDepth)
        {
            var depth = 1;

            foreach (var column in columns)
            {
                if (column is GraphCollectionColumn innerColumns)
                    depth = Math.Max(depth, baseDepth + GetMaxDepthInternal(innerColumns.Columns, baseDepth + 1));
            }

            return depth;
        }
    }
}
