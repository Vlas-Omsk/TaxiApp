using System.Collections.Generic;

namespace TaxiApp.WindowsApp.Models.Graph
{
    public sealed class GraphCollectionColumn : IGraphColumn
    {
        public GraphCollectionColumn(string name, GraphColumnCollection columns)
        {
            Name = name;
            Columns = columns;
        }

        public GraphCollectionColumn(string name, IEnumerable<IGraphColumn> columns)
        {
            Name = name;
            Columns = new GraphColumnCollection(columns);
        }

        public string Name { get; }
        public GraphColumnCollection Columns { get; }
    }
}
