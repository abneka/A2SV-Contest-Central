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

        public Task<UserEntity?> GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}