using Application.DTOs.Common;
using Application.DTOs.Location;

namespace Application.DTOs.Group;

public class GroupResponseDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;
    public Guid LocationId { get; set; }
    public LocationResponseDto Location { get; set; } = null!;
}