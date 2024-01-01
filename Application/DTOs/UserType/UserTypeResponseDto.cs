using Application.DTOs.Common;
using Application.DTOs.User;

namespace Application.DTOs.UserType;

public class UserTypeResponseDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public int Priority { get; set; }
    public List<UserResponseDto> Users { get; set; } = new List<UserResponseDto>();
}