using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

class ContestRepository : GenericRepository<ContestEntity>, IContestRepository
{
    public ContestRepository(AppDBContext dbContext) : base(dbContext)
    {
    }
}