using Auth.Application.Dto;
using Auth.Application.Services;
using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        private readonly IJwtTokenService _tokens;
        private readonly IUserRepository _users;

        public AuthController(AuthService auth, IJwtTokenService tokens, IUserRepository users)
        {
            _auth = auth;
            _tokens = tokens;
            _users = users;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new User { Username = dto.Username, Email = dto.Email };
            await _auth.RegisterAsync(user, dto.Password);
            return CreatedAtAction(null, null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _auth.AuthenticateAsync(dto.Username, dto.Password);
            if (user == null) return Unauthorized();

            var token = _tokens.GenerateToken(user);
            var expires = DateTime.UtcNow.AddHours(1);
            return Ok(new AuthResponseDto { Token = token, ExpiresAt = expires, Username = user.Username, Email = user.Email! });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var uidClaim = User.FindFirst("uid")?.Value;
            if (int.TryParse(uidClaim, out var userId))
            {
                var user = await _users.GetUserByIdAsync(userId);
                if (user != null)
                {
                    return Ok(new { user.Username, user.Email });
                }
            }
            return Unauthorized();
        }
    }
}
