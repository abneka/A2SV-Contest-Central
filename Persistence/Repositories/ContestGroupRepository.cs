using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Repositories.Common;

namespace Persistence.Repositories
{
    public class ContestGroupRepository : GenericRepository<ContestGroupEntity>, IContestGroupRepository
    {
        public ContestGroupRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}