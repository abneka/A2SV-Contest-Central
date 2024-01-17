using Application.DTOs.Auth;
using Application.Models.Authentication;
using MediatR;

namespace Application.Features.Auth.LogIn;

public class LoginUserCommand : IRequest<LoginResponse>
{
    public AuthRequest AuthRequest { get; set; } = null!;
}