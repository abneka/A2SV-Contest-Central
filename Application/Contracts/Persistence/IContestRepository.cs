using Application.Contracts.Persistence.Common;
using Application.DTOs.Contest;
using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IContestRepository : IGenericRepository<ContestEntity>
    {
    }
}