namespace Application.DTOs.Auth;

public class ForgetPasswordRequest
{
    public string ConfirmationCode { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}