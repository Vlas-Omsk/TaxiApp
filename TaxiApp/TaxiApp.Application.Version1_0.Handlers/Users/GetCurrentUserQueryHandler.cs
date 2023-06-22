using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Services;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.Handlers.Users
{
    internal sealed class GetCurrentUserQueryHandler : RequestHandler<GetCurrentUserQuery, UserDTO>
    {
        private readonly SecurityService _securityService;

        public GetCurrentUserQueryHandler(
            SecurityService securityService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _securityService = securityService;
        }

        protected override async Task<Response<UserDTO>> ExecuteOverride(GetCurrentUserQuery request)
        {
            var user = await _securityService.GetCurrentUser();

            return Success(new UserDTO(
                user.Login,
                new FullName(
                    user.LastName,
                    user.FirstName,
                    user.Patronymic
                ),
                user.Role
            ));
        }

        protected override async Task VerifyOverride(GetCurrentUserQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
        }
    }
}
