using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Auth.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.ForgetPasswordRequest.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .MustAsync(async (email, cancellationToken) =>
            {
                var user = await userRepository.GetUserByEmail(email);
                return user != null;
            }).WithMessage("Email not found or not confirmed.");

        RuleFor(x => x.ForgetPasswordRequest.NewPassword)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");

        RuleFor(x => x.ForgetPasswordRequest.ConfirmationCode)
            .NotEmpty().WithMessage("Confirmation code is required");
    }
}