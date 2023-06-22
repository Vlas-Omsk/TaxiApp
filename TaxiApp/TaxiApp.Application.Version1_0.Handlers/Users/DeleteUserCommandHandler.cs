using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Users
{
    internal sealed class DeleteUserCommandHandler : RequestHandler<DeleteUserCommand, bool>
    {
        private readonly UsersService _usersService;

        public DeleteUserCommandHandler(
            UsersService usersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _usersService = usersService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(DeleteUserCommand request)
        {
            await _usersService.Delete(request.Login);

            return Success(true);
        }

        protected override async Task VerifyOverride(DeleteUserCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
