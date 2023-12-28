using Application.Contracts.Persistence;
using Domain.Entities;
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

        
    }
}