using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Services;
using TaxiApp.Application.Version1.DTO;
using TaxiApp.Application.Version1.Queries;

namespace TaxiApp.Application.Version1.Users.Queries.GetCurrentUser
{
    internal sealed class GetCurrentUserQueryHandler : RequestHandler<GetCurrentUserQuery, UserDTO>
    {
        private readonly SecurityService _securityService;

        public GetCurrentUserQueryHandler(SecurityService securityService)
        {
            _securityService = securityService;
        }

        protected override async Task<Response<UserDTO>> ExecuteOverride(GetCurrentUserQuery request)
        {
            var user = await _securityService.GetCurrentUserInfo();

            return Success(new UserDTO(
                user.Login,
                user.Role
            ));
        }
    }
}
