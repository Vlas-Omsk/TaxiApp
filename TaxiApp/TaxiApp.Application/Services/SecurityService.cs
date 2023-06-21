using TaxiApp.Domain.Models;
using TaxiApp.Domain.Services;
using TaxiApp.Server.Abstractions;

namespace TaxiApp.Application.Services
{
    public sealed class SecurityService
    {
        private readonly IRequestContext _requestContext;
        private readonly UsersService _usersService;
        private User _user;
        private bool _userInitialized = false;

        public SecurityService(
            IRequestContext requestContext,
            UsersService usersService
        )
        {
            _requestContext = requestContext;
            _usersService = usersService;
        }

        public async Task<User> GetCurrentUser()
        {
            if (_userInitialized)
                return _user;

            _user = await _usersService.Get(
                _requestContext.Login,
                _requestContext.Password
            );
            _userInitialized = true;

            return _user;
        }
    }
}
