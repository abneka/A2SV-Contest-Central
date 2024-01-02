using Domain.Entities;
using Application.Contracts.Persistence;
using Application.DTOs.User;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;

namespace Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    { 
        private readonly AppDBContext _dbContext;

        public UserRepository(AppDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserEntity?> GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public override async Task<IReadOnlyList<UserEntity>> GetAllAsync()
        {
            var query = _dbContext.Users.Include(u => u.Group).ThenInclude(g => g.Location).Include(u => u.UserQuestionResults).AsQueryable();
            
            return await query.ToListAsync();
        }

        public async Task<UserEntity?> GetUserIdByCodeforcesHandle(string codeforcesHandle)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.CodeforcesHandle == codeforcesHandle);
            return user.Id;
        }
        
        
    }
}