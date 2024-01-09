using Application.Contracts.Persistence.Common;
using Application.DTOs.Contest;
using Domain.Entities;
using MediatR;

namespace Application.Contracts.Persistence
{
    public interface IContestRepository : IGenericRepository<ContestEntity>
    {

        Task<bool> ExistsContestGlobalIdAsync(string contest_id);
        Task<ContestEntity> GetContestByGlobalIdAsync(string contest_id);
        Task<Unit> UpdateContestByGlobalIdAsync(Guid contest_id, ContestEntity update_contest);
        Task<string> GetGlobalIdByContestGuid(Guid contest_id);
        public Task<List<ContestEntity>> GetContestsWithGroups();
        public Task<List<UserContestAndQuestionDto>> GetContestLeaderboard(Guid contest_id);
    }
}
