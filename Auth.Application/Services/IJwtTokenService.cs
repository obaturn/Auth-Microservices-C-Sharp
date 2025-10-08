using Auth.Domain.Entities;

namespace Auth.Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
