using TaxiApp.DataTypes;

namespace TaxiApp.Application.Abstractions
{
    public sealed class Error
    {
        public Error(ErrorType type, string message)
        {
            Type = type;
            Message = message;
        }

        private Error()
        {
        }

        public ErrorType Type { get; private set; }
        public string Message { get; private set; }
    }
}
