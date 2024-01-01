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

        public async Task<Unit> UpdateContestByGlobalIdAsync(string contest_id, ContestEntity update_contest)
        {
            // find contest using contest id and update it using a new ContestEntity object
            var existingContest = await _dbContext.Contests
                .FirstOrDefaultAsync(c => c.ContestGlobalId == contest_id);

            if (existingContest != null)
            {
                // Update existingContest properties with values from update_contest
                existingContest.Type = update_contest.Type;
                existingContest.DurationSeconds = update_contest.DurationSeconds;
                existingContest.StartTimeSeconds = update_contest.StartTimeSeconds;
                existingContest.RelativeTimeSeconds = update_contest.RelativeTimeSeconds;
                existingContest.PreparedBy = update_contest.PreparedBy;
                existingContest.WebsiteUrl = update_contest.WebsiteUrl;
                existingContest.Description = update_contest.Description;
                existingContest.Difficulty = update_contest.Difficulty;
                existingContest.Kind = update_contest.Kind;
                existingContest.Season = update_contest.Season;
                existingContest.Status = update_contest.Status;
                
                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }

            // Return Unit.Value or any appropriate result
            return Unit.Value;
        }
    }
}