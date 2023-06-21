using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Services;
using TaxiApp.DataTypes;

namespace TaxiApp.Application
{
    public sealed class AccessVerifier
    {
        private readonly SecurityService _securityService;

        public AccessVerifier(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public bool HasErrors => Error != null;
        public Error Error { get; set; }

        public async Task HasRole(params UserRole[] roles)
        {
            if (HasErrors)
                return;

            var userInfo = await _securityService.GetCurrentUser();

            if (!roles.Any(x => x == userInfo.Role))
                Error = Errors.Users.DoesNotHaveAccess;
        }

        public async Task NotAnonymouse()
        {
            if (HasErrors)
                return;

            var userInfo = await _securityService.GetCurrentUser();

            if (userInfo == null)
                Error = Errors.Users.AnonymouseAccessNotAllowed;
        }

        public async Task NotCurrent(string userLogin)
        {
            if (HasErrors)
                return;

            var userInfo = await _securityService.GetCurrentUser();

            if (userInfo.Login == userLogin)
                Error = Errors.Users.UnableToChangeCurrentEmployee;
        }
    }
}
