using Application.Contracts.Persistence.Common;
using Application.DTOs.Contest;
using Domain.Entities;
using MediatR;

namespace Application.Contracts.Persistence
{
    public interface IContestRepository : IGenericRepository<ContestEntity>
    {

        public Task<bool> ExistsContestGlobalIdAsync(string contest_id);
        public Task<ContestEntity> GetContestByGlobalIdAsync(string contest_id);
        public Task<Unit> UpdateContestByGlobalIdAsync(Guid contest_id, ContestEntity update_contest);
        public Task<string> GetGlobalIdByContestGuid(Guid contest_id);
        public Task<List<ContestEntity>> GetContestsWithGroups();
        public Task<List<UserContestAndQuestionDto>> GetContestLeaderboard(Guid contest_id);
    }
}
