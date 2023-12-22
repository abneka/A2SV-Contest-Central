using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Auth.ForgetPassword;

public class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
{
    public ForgetPasswordCommandValidator(IUserRepository userRepository)
    {
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("{PropertyName} is invalid.")
            .MustAsync(async (email, cancellationToken) =>
            {
                var user = await userRepository.GetUserByEmail(email);
                return user != null;
            }).WithMessage("Email not found or not confirmed.");
    }
}