using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUserTypeRepository : IGenericRepository<UserTypeEntity>
{
    public Task<Guid> GetUserTypeIdByUserTypeName(string user_type_name);
}