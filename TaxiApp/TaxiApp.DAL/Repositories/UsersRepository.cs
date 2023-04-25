using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using TaxiApp.DAL.Abstractions.Models;
using TaxiApp.DAL.Abstractions.Repositories;
using TaxiApp.DAL.Entities;

namespace TaxiApp.DAL.Repositories
{
    public sealed class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<UserModel> Get(string login, string password)
        {
            var passwordHash = ComputeHash(password);
            var userEntity = await _applicationDbContext.Users
                .Where(x => 
                    x.Login == login &&
                    x.PasswordHash.SequenceEqual(passwordHash)
                )
                .FirstOrDefaultAsync();

            if (userEntity == null)
                return null;

            return new UserModel
            {
                Login = userEntity.Login,
                Role = userEntity.Role,
            };
        }

        public async Task Add(UserModel user)
        {
            await _applicationDbContext.Users.AddAsync(new UserEntity()
            {
                Login = user.Login,
                Role = user.Role,
                PasswordHash = ComputeHash(user.Password)
            });

            _applicationDbContext.SaveChanges();
        }

        private static byte[] ComputeHash(string value)
        {
            if (value == null || value.Length == 0)
                return Array.Empty<byte>();

            using var sha256 = SHA256.Create();

            return sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}
