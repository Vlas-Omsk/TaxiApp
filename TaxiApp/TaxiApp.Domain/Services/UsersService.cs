using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL;
using TaxiApp.DAL.Entities;
using TaxiApp.DataTypes;

namespace TaxiApp.Domain.Services
{
    public sealed class UsersService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UsersService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<UserEntity> GetAll()
        {
            return _applicationDbContext.Users;
        }

        public async Task<UserEntity> Get(string login, string password)
        {
            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }

        public async Task Create(
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
