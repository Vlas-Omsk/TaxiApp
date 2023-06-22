using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Users
{
    internal sealed class UpdateUserCommandHandler : RequestHandler<UpdateUserCommand, bool>
    {
        private readonly UsersService _usersService;

        public UpdateUserCommandHandler(
            UsersService usersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _usersService = usersService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(UpdateUserCommand request)
        {
            await _usersService.Update(
                request.Login,
                request.Password,
                request.FullName,
                request.Inn,
                request.Passport,
                request.Address,
                request.Role,
                request.AdditionalInfo
            );

            return Success(true);
        }

        protected override async Task VerifyOverride(UpdateUserCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
