using Domain.Common;

namespace Domain.Entities;

public class GroupEntity : BaseDomainEntity
{
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;
    public string Generation { get; set; } = null!;
    
    public string Year { get; set; } = null!;
    public Guid LocationId { get; set; }
    public LocationEntity Location { get; set; } = null!;
    public List<UserEntity> Members { get; set; } = new List<UserEntity>();
    public List<ContestGroupEntity> Contests { get; set; } = new List<ContestGroupEntity>();
}