using Application.DTOs.User;

namespace Application.DTOs.Auth
{
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!; 
        public string LastName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
