using Domain.Common;

namespace Domain.Entities;

public class ContestGroupEntity : BaseDomainEntity
{
   
    public Guid ContestId { get; set; }
    
    public int GroupNumber { get; set; }
}