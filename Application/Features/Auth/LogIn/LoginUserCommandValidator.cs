using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Auth.LogIn;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(p => p.AuthRequest.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .MustAsync(async (email, token) =>
            {
                var userExists = await userRepository.GetUserByEmail(email);
                return userExists is { IsVerified: false };
            }).WithMessage("User is already verified.");

        RuleFor(p => p.AuthRequest.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(6).WithMessage("{PropertyName} must not be less than {MinLength} characters.")
            .MaximumLength(50).WithMessage("{PropertyName} must not be greater than {MaxLength} characters.");
    }
}