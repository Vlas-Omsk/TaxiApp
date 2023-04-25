using TaxiApp.Domain.Abstractions.Models;

namespace TaxiApp.Domain.Abstractions.Services
{
    public interface IUsersService
    {
        Task<UserInfo> Get(string login, string password);
        Task Add(User user);
    }
}
