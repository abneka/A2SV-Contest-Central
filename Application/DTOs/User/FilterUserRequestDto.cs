using Application.DTOs.Common;

namespace Application.DTOs.User;

public class FilterUserRequestDto : FilterRequestDto
{
    public string? UserType { get; set; }
}