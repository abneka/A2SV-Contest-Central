using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUserContestResultRepository : IGenericRepository<UserContestResultEntity>
{
    public Task<UserContestResultEntity> GetUserContestResultByUserIdAsync(Guid userId);
    public Task<List<UserContestResultEntity>> GetUserContestResutByGroupIdAsync(Guid groupId);
    public Task<List<UserContestResultEntity>> GetUserContestResutByLocationIdAsync(Guid locationId);
}