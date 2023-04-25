using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application
{
    internal static class Results
    {
        public static Response<T> Success<T>(T value)
        {
            return new Response<T>()
            {
                Value = value,
                Error = null,
                Success = true
            };
        }

        public static Response<T> Fail<T>(Error error)
        {
            return new Response<T>()
            {
                Value = default,
                Error = error,
                Success = false
            };
        }
    }
}
