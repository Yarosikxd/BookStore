using Core.Models;

namespace Core.Abstraction.Repository
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetByEmailUserAsync(string email);
    }
}
