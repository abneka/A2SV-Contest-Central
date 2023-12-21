using Application.DTOs.User;

namespace Application.DTOs.Auth
{
    public class AuthResponse : UserResponseDto
    {
        public string Token { get; set; } = null!;
    }
}
