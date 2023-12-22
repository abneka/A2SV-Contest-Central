using Domain.Entities;
using Persistence.Repositories.Common;

namespace Persistence.Repositories
{
    public class ContestRepository : GenericRepository<ContestEntity>
    {
        protected ContestRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}