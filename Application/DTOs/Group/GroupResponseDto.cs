using Application.DTOs.Common;
using Application.DTOs.Location;

namespace Application.DTOs.Group;

public class GroupResponseDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;
    public string Generation { get; set; } = null!;
    public Guid LocationId { get; set; }
    public LocationResponseDto Location { get; set; } = null!;
}