using Application.DTOs.Common;
using Application.DTOs.Group;

namespace Application.DTOs.User;

public class UserResponseDto : BaseDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Guid GroupId { get; set; }
    public GroupResponseDto Group { get; set; } = null!;
    public string CodeforcesHandle { get; set; } = null!;
}