using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Users
{
    internal sealed class GetUserInfoQueryHandler : RequestHandler<GetUserInfoQuery, UserInfoDTO>
    {
        private readonly UsersService _usersService;

        public GetUserInfoQueryHandler(
            UsersService usersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _usersService = usersService;
        }

        protected override async Task<Response<UserInfoDTO>> ExecuteOverride(GetUserInfoQuery request)
        {
            var user = await _usersService.GetAll()
                .FirstOrDefaultAsync(x => x.Login == request.Login);

            return Success(new UserInfoDTO(
                user.Login,
                new FullName(
                    user.LastName,
                    user.FirstName,
                    user.Patronymic
                ),
                user.Inn,
                user.Passport,
                user.Address,
                user.Role,
                user.AdditionalInfo
            ));
        }

        protected override async Task VerifyOverride(GetUserInfoQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
