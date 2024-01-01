using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class UserContestResultRepository : GenericRepository<UserContestResultEntity>, IUserContestResultRepository
{
    private readonly AppDBContext _dbContext;
    
    public UserContestResultRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    // get a user's all contest results
    public async Task<List<UserContestResultEntity>> GetUserContestResultsByUserIdAsync(Guid userId)
    {
        var res = await _dbContext.UserContestResults.Where(ucr => ucr.UserId == userId).ToListAsync();

        return res;
    }

    public async Task<List<UserContestResultEntity>> GetUserContestResultByGroupIdAsync(Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserContestResultEntity>> GetUserContestResultByLocationIdAsync(Guid locationId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserContestResultEntity> GetUserContestResultByUserIdAndContestIdAsync(Guid userId, Guid contestId)
    {
        var res = await _dbContext.UserContestResults.FirstOrDefaultAsync(ucr => ucr.UserId == userId && ucr.ContestId == contestId);

        return res;
    }
}