using FluentValidation;

namespace Application.Features.User.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(x => x.AuthRequest.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.AuthRequest.Password).NotEmpty().MinimumLength(8);
    }
}