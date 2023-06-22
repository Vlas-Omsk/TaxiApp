namespace TaxiApp.WindowsApp.Models.Graph
{
    public sealed class GraphValueColumn : IGraphColumn
    {
        public GraphValueColumn(string name, IGraphValue value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public IGraphValue Value { get; }
    }
}
