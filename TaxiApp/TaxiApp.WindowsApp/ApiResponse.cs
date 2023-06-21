namespace TaxiApp.WindowsApp
{
    internal sealed record ApiResponse<T>(
        T Value,
        string Message,
        bool Success
    );
}
