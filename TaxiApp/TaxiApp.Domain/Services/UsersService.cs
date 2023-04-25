using TaxiApp.DAL.Abstractions.Models;
using TaxiApp.DAL.Abstractions.Repositories;
using TaxiApp.Domain.Abstractions.Models;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Domain.Services
{
    public sealed class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserInfo> Get(string login, string password)
        {
            var user = await _usersRepository.Get(login, password);

            if (user == null)
                return null;

            return new UserInfo(user.Login, user.Role);
        }

        public async Task Add(User user)
        {
            await _usersRepository.Add(new UserModel()
            {
                Login = user.Login,
                Role = user.Role,
                Password = user.Password
            });
        }
    }
}
