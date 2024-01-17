using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Auth.LogIn;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(p => p.AuthRequest.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.")
            .MustAsync(async (email, token) =>
            {
                var userExists = await unitOfWork.UserRepository.GetUserByEmail(email);
                return userExists != null;
            })
            .WithMessage("Invalid email or password");

        RuleFor(p => p.AuthRequest.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(6).WithMessage("{PropertyName} must not be less than {MinLength} characters.")
            .MaximumLength(50).WithMessage("{PropertyName} must not be greater than {MaxLength} characters.");
    }
}