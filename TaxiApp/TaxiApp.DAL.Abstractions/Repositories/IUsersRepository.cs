using TaxiApp.DAL.Abstractions.Models;

namespace TaxiApp.DAL.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task<UserModel> Get(string login, string password);
        Task Add(UserModel user);
    }
}
