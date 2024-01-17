using Application.Models.Authentication;

namespace Application.Contracts.Infrastructure
{
    public interface IAuthenticationService
    {
        public LoginResponse Login(LoginRequest loginRequest);

    }
}
