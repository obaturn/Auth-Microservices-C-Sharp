namespace Auth.Application.Services
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string hashed, string password) => BCrypt.Net.BCrypt.Verify(password, hashed);
    }
}
