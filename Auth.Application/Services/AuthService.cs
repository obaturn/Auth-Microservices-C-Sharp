using Auth.Domain.Entities;
using Auth.Domain.Repositories;

namespace Auth.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;

        public AuthService(IUserRepository users, IPasswordHasher hasher)
        {
            _users = users;
            _hasher = hasher;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _users.GetUserByUsernameAsync(username);
            if (user == null) return null;

            if (_hasher.Verify(user.HashedPassword, password)) return user;
            return null;
        }

        public async Task RegisterAsync(User user, string plainPassword)
        {
            user.HashedPassword = _hasher.Hash(plainPassword);
            await _users.AddUserAsync(user);
            await _users.SaveChangesAsync();
        }
    }
}
