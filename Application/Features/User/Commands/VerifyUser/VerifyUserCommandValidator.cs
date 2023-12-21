using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.User.Commands.VerifyUser;

public class VerifyUserCommandValidator : AbstractValidator<VerifyUserCommand>
{
    public VerifyUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .MustAsync(async (email, token) =>
            {
                var userExists = await userRepository.GetUserByEmail(email);
                return userExists is { IsVerified: false };
            }).WithMessage("User is already verified.");
        
        RuleFor(v => v.Token)
            .NotEmpty().WithMessage("Token is required.");
    }
}