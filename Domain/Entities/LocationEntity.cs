using Domain.Common;

namespace Domain.Entities;

public class LocationEntity : BaseDomainEntity
{
    public string Location { get; set; } = null!;
    public List<GroupEntity> Groups { get; set; } = new();
}