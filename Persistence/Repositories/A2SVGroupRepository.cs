using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class A2SVGroupRepository : GenericRepository<GroupEntity>, IA2SVGroupRepository
{
    private readonly AppDBContext _dbContext;
    public A2SVGroupRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GroupEntity>> GetAllGroupsWithMembers()
    {
        return await _dbContext.Groups.Include(g => g.Members).Include(g => g.Location).ToListAsync();
    }

    public async Task<Guid> GetGroupIdByGroupName(string group_name)
    {
        var group = await _dbContext.Groups
                        .Where(group => group.Name == group_name)
                        .FirstOrDefaultAsync();
        
        if (group != null)
        {
            return group.Id;
        }
        return Guid.Empty;
    }
}