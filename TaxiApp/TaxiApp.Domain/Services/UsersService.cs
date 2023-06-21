using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL;
using TaxiApp.DAL.Entities;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Models;

namespace TaxiApp.Domain.Services
{
    public sealed class UsersService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UsersService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User> Get(string login, string password)
        {
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (user == null)
                return null;

            return new User(user.Login, user.Role);
        }

        public async Task Add(
            string login,
            string password,
            UserRole role
        )
        {
            await _applicationDbContext.Users.AddAsync(new UserEntity()
            {
                Login = login,
                Password = password,
                Role = role,
            });
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
