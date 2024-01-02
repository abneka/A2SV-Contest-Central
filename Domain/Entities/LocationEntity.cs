using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

public class LocationEntity : BaseDomainEntity
{
    public string Location { get; set; } = null!;
    public string Country { get; set; } = null!;
    [JsonIgnore] 
    public List<GroupEntity>? Groups { get; set; }
}