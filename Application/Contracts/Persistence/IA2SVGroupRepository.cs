using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IA2SVGroupRepository : IGenericRepository<GroupEntity>
{
    Task<List<GroupEntity>> GetAllGroupsWithMembers();
}