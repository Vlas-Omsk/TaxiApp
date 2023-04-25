using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1.Commands;
using TaxiApp.Domain.Abstractions.Models;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Application.Version1.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : RequestHandler<CreateUserCommand, bool>
    {
        private readonly IUsersService _usersService;

        public CreateUserCommandHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(CreateUserCommand request)
        {
            await _usersService.Add(new User(
                request.Login,
                request.Password,
                request.Role
            ));

            return Success(true);
        }
    }
}
