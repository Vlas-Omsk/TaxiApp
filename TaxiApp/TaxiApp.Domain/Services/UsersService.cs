using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
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
            var passwordHash = ComputePasswordHash(password);

            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Login == login && x.PasswordHash == passwordHash);
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
                PasswordHash = ComputePasswordHash(password),
                Role = role,
            });
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Update(
            string login,
            string password,
            FullName fullName,
            string inn,
            string passport,
            string address,
            UserRole? role,
            string additionalInfo
        )
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Login == login);

            if (password != null)
                user.PasswordHash = ComputePasswordHash(password);
            if (fullName != null)
            {
                user.LastName = fullName.LastName;
                user.FirstName = fullName.FirstName;
                user.Patronymic = fullName.Patronymic;
            }
            if (inn != null)
                user.Inn = inn;
            if (passport != null)
                user.Passport = passport;
            if (address != null)
                user.Address = address;
            if (role.HasValue)
                user.Role = role.Value;
            if (additionalInfo != null)
                user.AdditionalInfo = additionalInfo;

            _applicationDbContext.Users.Update(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(string login)
        {
            _applicationDbContext.Users.Remove(
                await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Login == login)
            );
            await _applicationDbContext.SaveChangesAsync();
        }

        private static byte[] ComputePasswordHash(string value)
        {
            using var sha256 = SHA256.Create();

            return sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}
