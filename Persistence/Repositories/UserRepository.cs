using Application.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
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

        public override async Task<IReadOnlyList<UserEntity>> GetAllAsync()
        {
            var query = await _dbContext.Users.Include(u => u.Group).ThenInclude(g => g.Location)
                .Include(u => u.UserQuestionResults).Include(u => u.UserType).ToListAsync();

            return query;
        }

        public override async Task<UserEntity?> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.Users
                .Include(u => u.Group)
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public Task<UserEntity?> GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Guid?> GetUserIdByCodeforcesHandle(string codeforcesHandle)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.CodeforcesHandle == codeforcesHandle);
            return user?.Id;
        }
        public async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
            
            return user;
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            
            return user;
        }
        
    }
}