using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiApp.WindowsApp.Models.Graph
{
    public sealed class GraphData
    {
        public GraphData(GraphColumnCollection columns)
        {
            Columns = columns;
            MaxValue = GetMaxValueInternal(columns);
        }

        public GraphData(GraphColumnCollection columns, double maxValue)
        {
            Columns = columns;
            MaxValue = maxValue;
        }

        public GraphColumnCollection Columns { get; }
        public double MaxValue { get; private set; }

        public void Invert()
        {
            Columns.Invert();
        }

        public void FindAverage()
        {
            Columns.FindAverage();
        }

        public int GetMaxDepth()
        {
            return Columns.GetMaxDepth();
        }

        public void EnsureHasDoubleDepth()
        {
            Columns.ThrowIfNotDoubleDepth();
        }

        public void Add(GraphData data, string name)
        {
            Columns.Add(new GraphCollectionColumn(name, data.Columns));
            MaxValue = Math.Max(data.MaxValue, MaxValue);
        }

        private double GetMaxValueInternal(IEnumerable<IGraphColumn> columns)
        {
            if (!columns.Any())
                return 0;

            return columns.Max(x =>
            {
                if (x is GraphCollectionColumn innerColumns)
                    return GetMaxValueInternal(innerColumns.Columns);
                else if (x is GraphValueColumn valueColumn)
                    return valueColumn.Value.Value;
                else
                    throw new InvalidOperationException();
            });
        }

        public static GraphData FromArray<TOuter, TInner>(
            TOuter[] statistics,
            Func<TOuter, TInner[]> getInnerItems,
            Func<TOuter, TInner, IGraphValue> getValue,
            Func<TOuter, string> getOuterName,
            Func<TInner, string> getInnerName,
            double? maxValue = null
        )
        {
            if (statistics.Length == 0)
                return maxValue.HasValue ?
                    new GraphData(new GraphColumnCollection(Array.Empty<IGraphColumn>()), maxValue.Value) :
                    new GraphData(new GraphColumnCollection(Array.Empty<IGraphColumn>()));

            var outerNames = statistics
                .Select(getOuterName)
                .ToArray();
            var innerNames = getInnerItems(statistics[0])
                .Select(getInnerName)
                .ToArray();

            var data = statistics
                .Select((x, i) =>
                {
                    var keyValues = getInnerItems(x)
                        .Select((c, j) =>
                            new GraphValueColumn(innerNames[j], getValue(x, c))
                        );

                    return new GraphCollectionColumn(outerNames[i], keyValues);
                });

            return maxValue.HasValue ?
                new GraphData(new GraphColumnCollection(data), maxValue.Value) :
                new GraphData(new GraphColumnCollection(data));
        }

        public static GraphData FromArray<TOuter>(
            TOuter[] statistics,
            Func<TOuter, IGraphValue> getValue,
            Func<TOuter, string> getOuterName,
            double? maxValue = null
        )
        {
            var outerNames = statistics
                .Select(getOuterName)
                .ToArray();

            var data = statistics
                .Select((x, i) =>
                    new GraphValueColumn(outerNames[i], getValue(x))
                );

            return maxValue.HasValue ?
                new GraphData(new GraphColumnCollection(data), maxValue.Value) :
                new GraphData(new GraphColumnCollection(data));
        }
    }
}
