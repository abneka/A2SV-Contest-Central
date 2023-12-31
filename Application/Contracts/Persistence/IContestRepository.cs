using Application.Contracts.Persistence.Common;
using Domain.Entities;
using MediatR;

namespace Application.Contracts.Persistence
{
    public interface IContestRepository : IGenericRepository<ContestEntity>
    {
        Task<bool> ExistsContestGlobalIdAsync(string contest_id);
        Task<ContestEntity> GetContestByGlobalIdAsync(string contest_id);
        Task<Unit> UpdateContestByGlobalIdAsync(string contest_id, ContestEntity update_contest);
    }
}
