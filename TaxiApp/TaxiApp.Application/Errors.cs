using TaxiApp.Application.Abstractions;
using TaxiApp.DataTypes;

namespace TaxiApp.Application
{
    internal static class Errors
    {
        internal static class Users
        {
            public static Error DoesNotHaveAccess { get; } = new(ErrorType.DoesNotHaveAccess, null);
            public static Error AnonymouseAccessNotAllowed { get; } = new(ErrorType.AnonymouseAccessNotAllowed, null);
            public static Error UnableToChangeCurrentEmployee { get; } = new(ErrorType.UnableToChangeCurrentEmployee, null);
        }
    }
}
