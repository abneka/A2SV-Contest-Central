using System.Runtime.CompilerServices;
using Domain.Common;

namespace Domain.Entities;

public class GroupEntity : BaseDomainEntity
{
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;
    public Guid LocationId { get; set; }
    public LocationEntity Location { get; set; } = null!;
}