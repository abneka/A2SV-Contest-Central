using Application.DTOs.Common;
using Application.DTOs.Group;
using Application.DTOs.UserType;

namespace Application.DTOs.User;

public class UserResponseDto : BaseDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ProfilePicture { get; set; } = null!;
    public string? CoverPicture { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public string Phone { get; set; } = null!;
    public Guid UserTypeId { get; set; }
    public UserTypeResponseDto UserType { get; set; } = null!;
    public Guid GroupId { get; set; }
    public GroupResponseDto Group { get; set; } = null!;
    public string CodeforcesHandle { get; set; } = null!;
    public int NumberOfProblemsTaken { get; set; }
    public int NumberOfProblemsSolved { get; set; }
}