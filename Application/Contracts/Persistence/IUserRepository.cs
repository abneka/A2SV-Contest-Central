using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    public Task<UserEntity?> GetUserByEmail(string email);
    public Task<UserEntity?> GetUserIdByCodeforcesHandle(string codeforcesHandle);
}