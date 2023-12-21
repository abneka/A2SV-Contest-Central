using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.User.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommand : IRequest<Unit>
{
    public AuthRequest AuthRequest { get; set; } = null!;
}