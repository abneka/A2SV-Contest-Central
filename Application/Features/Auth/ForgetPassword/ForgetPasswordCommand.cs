using MediatR;

namespace Application.Features.Auth.ForgetPassword;

public class ForgetPasswordCommand : IRequest<Unit>
{
    public string Email { get; set; } = null!;
}