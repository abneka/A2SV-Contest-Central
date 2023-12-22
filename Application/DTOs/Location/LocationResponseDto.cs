using Application.DTOs.Common;
using Application.DTOs.Group;

namespace Application.DTOs.Location;

public class LocationResponseDto : BaseDto
{
    public string Location { get; set; } = null!;
    public List<GroupResponseDto> Groups { get; set; } = new();
}