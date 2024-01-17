namespace Application.Models.Authentication;

public class LoginRequest
{
    //data that comes from user to login
    public string Email { get; set; } = string.Empty;
    public string LoginPassword { get; set; } = string.Empty;

    //data that comes from database
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string OriginalPassword { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public int Priority { get; set; }
}
