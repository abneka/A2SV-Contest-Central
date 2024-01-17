using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class UserTypeRepository : GenericRepository<UserTypeEntity>, IUserTypeRepository
{
    private readonly AppDBContext _dbContext;
    public UserTypeRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> GetUserTypeIdByUserTypeName(string user_type_name)
    {
        var role = await _dbContext.UserTypeEntity
                .Where(type => type.Name == user_type_name)
                .FirstOrDefaultAsync();

        if (role != null)
        {
            return role.Id;
        }
        return Guid.Empty;
    }

    public async Task<string> GetUserTypeNameById(Guid user_type_id)
    {
        var role = await _dbContext.UserTypeEntity.FindAsync(user_type_id);

        if (role != null)
        {
            return role.Name;
        }
        return string.Empty;
    }
}