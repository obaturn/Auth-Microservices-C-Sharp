using Auth.Domain.Entities;
using Auth.Domain.Repositories;

namespace Auth.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _users;

        public AuthService(IUserRepository users)
        {
            _users = users;
        }

        
        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _users.GetUserByUsernameAsync(username);
            if (user == null) return null;

            if (user.HashedPassword == password) return user;
            return null;
        }

        public async Task RegisterAsync(User user, string plainPassword)
        {
        
            user.HashedPassword = plainPassword;
            await _users.AddUserAsync(user);
            await _users.SaveChangesAsync();
        }
    }
}
