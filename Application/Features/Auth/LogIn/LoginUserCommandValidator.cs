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
                return userExists != null;
            })
            .WithMessage("No user found with this email.");

        RuleFor(p => p.AuthRequest.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(6).WithMessage("{PropertyName} must not be less than {MinLength} characters.")
            .MaximumLength(50).WithMessage("{PropertyName} must not be greater than {MaxLength} characters.");
    }
}