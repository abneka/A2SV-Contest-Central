using FluentValidation;

namespace Application.Features.Auth.LogIn;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(p => p.AuthRequest.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("{PropertyName} is not valid.");

        RuleFor(p => p.AuthRequest.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(6).WithMessage("{PropertyName} must not be less than {MinLength} characters.")
            .MaximumLength(50).WithMessage("{PropertyName} must not be greater than {MaxLength} characters.");
    }
}