using Application.DTOs.Common;

namespace Application.DTOs.User;

public class UserResponseDto : BaseDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Group { get; set; } = null!;
    public string CodeforcesHandle { get; set; } = null!;
}