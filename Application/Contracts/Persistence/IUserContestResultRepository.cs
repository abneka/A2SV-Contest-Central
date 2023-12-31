using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUserContestResultRepository : IGenericRepository<UserContestResultEntity>
{
    public Task<List<UserContestResultEntity>> GetUserContestResultsByUserIdAsync(Guid userId);
    public Task<List<UserContestResultEntity>> GetUserContestResultByGroupIdAsync(Guid groupId);
    public Task<List<UserContestResultEntity>> GetUserContestResultByLocationIdAsync(Guid locationId);
    public Task<UserContestResultEntity> GetUserContestResultByUserIdAndContestIdAsync(Guid userId, Guid contestId);
}