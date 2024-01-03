using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class A2SVGroupRepository : GenericRepository<GroupEntity>, IA2SVGroupRepository
{
    private new readonly AppDBContext _dbContext;
    public A2SVGroupRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GroupEntity>> GetAllGroupsWithMembers()
    {
        return await _dbContext.Groups.Include(g => g.Members).Include(g => g.Location).ToListAsync();
    }
}