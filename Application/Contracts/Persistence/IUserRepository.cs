using Application.Contracts.Persistence.Common;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    public Task<UserEntity?> GetUserByEmail(string email);
    public Task<Guid?> GetUserIdByCodeforcesHandle(string codeforcesHandle);
    public Task<UserEntity> GetUserByUsernameAsync(string username);
    public Task<UserEntity> GetUserByEmailAsync(string email);
}