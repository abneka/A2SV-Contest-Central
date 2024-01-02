using Application.DTOs.Common;
using Application.DTOs.Group;
using Newtonsoft.Json;

namespace Application.DTOs.Location;

public class LocationResponseDto : BaseDto
{
    public string Location { get; set; } = null!;
    public string Country { get; set; } = null!;
}