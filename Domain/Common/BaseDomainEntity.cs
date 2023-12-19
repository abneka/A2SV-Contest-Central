namespace Domain.Common;

public class BaseDomainEntity
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;
}