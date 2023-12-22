using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.ResetPassword;

public class ResetPasswordCommand : IRequest<string>
{
    public ForgetPasswordRequest ForgetPasswordRequest { get; set; } = null!;
}