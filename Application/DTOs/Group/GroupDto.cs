using Application.DTOs.Common;
using Application.DTOs.Location;
using Domain.Entities;

namespace Application.DTOs.Group;

public class GroupDto
{
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;
    public Guid LocationId { get; set; }
    public LocationResponseDto? Location { get; set; }
    public string Generation { get; set; } = null!;
}