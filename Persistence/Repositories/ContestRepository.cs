using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;


namespace Persistence.Repositories
{
    public class ContestRepository : GenericRepository<ContestEntity>, IContestRepository
    {
        private AppDBContext _dbContext;
        public ContestRepository(AppDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsContestGlobalIdAsync(string contest_id)
        {
            var item = await _dbContext.Contests
            .Where(contest => contest.ContestGlobalId == contest_id).FirstOrDefaultAsync();;
            
            return item != null;
        }
    }
}