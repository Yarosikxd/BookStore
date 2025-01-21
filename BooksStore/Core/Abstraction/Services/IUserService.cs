using Core.Models;

namespace Core.Abstraction.Services
{
    public  interface IUserService
    {
        Task Register(string userName, string email, string password);
        Task<string> Login (string email, string password);
    }
}
