using Application.DTOs.ContestGroup;
using Application.DTOs.Group;

namespace Application.DTOs.User;

public class ContestUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CodeforcesHandle { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public GroupResponseDto UserGroup { get; set; } = null!;
}