using Application.DTOs.Common;

namespace Application.DTOs.Group;

public class GroupDto
{
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;
    public Guid LocationId { get; set; }
}