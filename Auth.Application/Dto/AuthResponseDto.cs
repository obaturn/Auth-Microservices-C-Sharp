namespace Auth.Application.Dto
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
