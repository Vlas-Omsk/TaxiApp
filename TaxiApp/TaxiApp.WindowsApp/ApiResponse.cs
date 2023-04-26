namespace TaxiApp.WindowsApp
{
    internal sealed class ApiResponse<T>
    {
        public ApiResponse(T value, string message, bool success)
        {
            Value = value;
            Message = message;
            Success = success;
        }

        public T Value { get; }
        public string Message { get; }
        public bool Success { get; }
    }
}
