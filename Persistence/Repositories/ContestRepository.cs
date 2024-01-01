using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories
{
    public class ContestRepository : GenericRepository<ContestEntity>, IContestRepository
    {
        private readonly AppDBContext _dbContext;
        public ContestRepository(AppDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> ExistsContestGlobalIdAsync(string contest_id)
        {
            var item = await GetContestByGlobalIdAsync(contest_id);
            return item != null;
        }

        public async Task<ContestEntity> GetContestByGlobalIdAsync(string contest_id)
        {
            var item = await _dbContext.Contests
            .Where(contest => contest.ContestGlobalId == contest_id).FirstOrDefaultAsync();
            return item;
        }

        public Task<Unit> UpdateContestByGlobalIdAsync(string contest_id, ContestEntity update_contest)
        {
            throw new NotImplementedException();
        }
    }
}