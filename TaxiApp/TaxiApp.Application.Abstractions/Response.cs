namespace TaxiApp.Application.Abstractions
{
    public sealed class Response<TResult> : IResponse
    {
        public Response()
        {
        }

        public TResult Value { get; set; }
        public Error Error { get; set; }
        public bool Success { get; set; }
    }
}
