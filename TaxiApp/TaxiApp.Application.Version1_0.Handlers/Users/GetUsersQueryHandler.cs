using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Users
{
    internal sealed class GetUsersQueryHandler : RequestHandler<GetUsersQuery, UserDTO[]>
    {
        private readonly UsersService _usersService;

        public GetUsersQueryHandler(
            UsersService usersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _usersService = usersService;
        }

        protected override async Task<Response<UserDTO[]>> ExecuteOverride(GetUsersQuery request)
        {
            var result = await _usersService.GetAll()
                .Select(x => new UserDTO(
                    x.Login,
                    new FullName(
                        x.LastName,
                        x.FirstName,
                        x.Patronymic
                    ),
                    x.Role
                ))
                .ToArrayAsync();

            return Success(result);
        }

        protected override async Task VerifyOverride(GetUsersQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
