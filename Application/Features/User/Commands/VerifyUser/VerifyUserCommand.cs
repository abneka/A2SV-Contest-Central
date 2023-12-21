using MediatR;

namespace Application.Features.User.Commands.VerifyUser;

public class VerifyUserCommand : IRequest<bool>
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}