using Application.DTOs.Auth;

namespace Application.Contracts.Persistence.Auth;

public interface IAuth
{
    public Task<AuthResponse> Login(AuthRequest authRequest);
}