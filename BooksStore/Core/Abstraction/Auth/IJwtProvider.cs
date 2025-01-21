using Core.Models;

namespace Core.Abstraction.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
