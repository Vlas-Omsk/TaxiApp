using TaxiApp.Application.Abstractions;
using TaxiApp.DataTypes;

namespace TaxiApp.Application
{
    public static class Errors
    {
        public static class Users
        {
            public static Error DoesNotHaveAccess { get; } = new(ErrorType.DoesNotHaveAccess, null);
            public static Error AnonymouseAccessNotAllowed { get; } = new(ErrorType.AnonymouseAccessNotAllowed, null);
            public static Error UnableToChangeCurrentEmployee { get; } = new(ErrorType.UnableToChangeCurrentEmployee, null);
        }

        public static class Cars
        {
            public static Error NotFound { get; } = new(ErrorType.CarNotFound, null);
        }

        public static class Drivers
        {
            public static Error NotFound { get; } = new(ErrorType.DriverNotFound, null);
        }
    }
}
