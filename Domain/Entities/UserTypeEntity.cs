using Domain.Common;

namespace Domain.Entities
{
    public class UserTypeEntity : BaseDomainEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }

    }
}
