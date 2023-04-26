using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Abstractions;
using TaxiApp.DAL.Abstractions.Entities;
using TaxiApp.Domain.Abstractions.Models;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Domain.Services
{
    public sealed class UsersService : IUsersService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UsersService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<UserInfo> Get(string login, string password)
        {
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (user == null)
                return null;

            return new UserInfo(user.Login, user.Role);
        }

        public async Task Add(User user)
        {
            await _applicationDbContext.Users.AddAsync(new UserEntity()
            {
                Login = user.Login,
                Role = user.Role,
                Password = user.Password
            });

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
