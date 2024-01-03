using Application.DTOs.Common;
using Application.DTOs.Location;
using Application.DTOs.User;
using System.Text.Json.Serialization;

namespace Application.DTOs.Group;

public class GroupResponseDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;
    public string Generation { get; set; } = null!;
    [JsonIgnore]
    public List<UserResponseDto> Members { get; set; } = new List<UserResponseDto>();
    public Guid LocationId { get; set; }
    public LocationResponseDto Location { get; set; } = null!;
}