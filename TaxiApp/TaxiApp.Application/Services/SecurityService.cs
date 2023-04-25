using TaxiApp.Domain.Abstractions.Models;
using TaxiApp.Domain.Abstractions.Services;
using TaxiApp.Server.Abstractions;

namespace TaxiApp.Application.Services
{
    internal sealed class SecurityService
    {
        private readonly IRequestContext _requestContext;
        private readonly IUsersService _usersService;
        private UserInfo _userInfo;
        private bool _userInitialized = false;

        public SecurityService(
            IRequestContext requestContext,
            IUsersService usersService
        )
        {
            _requestContext = requestContext;
            _usersService = usersService;
        }

        public async Task<UserInfo> GetCurrentUserInfo()
        {
            if (_userInitialized)
                return _userInfo;

            _userInfo = await _usersService.Get(
                _requestContext.Login,
                _requestContext.Password
            );
            _userInitialized = true;

            return _userInfo;
        }
    }
}
