namespace TaxiApp.WindowsApp.Models.Graph
{
    public sealed class GraphValue : IGraphValue
    {
        public GraphValue(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}
