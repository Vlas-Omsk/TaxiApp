using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Users
{
    internal sealed class CreateUserCommandHandler : RequestHandler<CreateUserCommand, bool>
    {
        private readonly UsersService _usersService;

        public CreateUserCommandHandler(
            UsersService usersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _usersService = usersService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(CreateUserCommand request)
        {
            await _usersService.Create(
                request.Login,
                request.Password,
                request.Role
            );

            return Success(true);
        }

        protected override Task VerifyOverride(CreateUserCommand request, AccessVerifier verifier)
        {
            return Task.CompletedTask;
        }
    }
}
