using Application.Contracts.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly AppDBContext _dbContext;

        protected GenericRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var item = await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<Unit> DeleteAsync(Guid Id)
        {
            var item = await GetByIdAsync(Id);
            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<bool> Exists(Guid id)
        {
            var item = await _dbContext.Set<T>().FindAsync(id);

            return item != null;
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var item = await _dbContext.Set<T>().FindAsync(id);
            return item;
        }

        public async Task<Unit> UpdateAsync(Guid id, T entity)
        {
            var item = await GetByIdAsync(id);
            // _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.Entry(item).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }

        //create new entity by list
        public async Task<Unit> CreateListAsync(IReadOnlyList<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            
            return Unit.Value;
        }

        public async Task<IReadOnlyList<T>> GetPagedEntitiesAsync(int skip, int take)
        {
            return await _dbContext
                .Set<T>()
                .OrderBy(c => c.GetType().GetProperty("Id").GetValue(c)) 
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetTotalEntitiesCount()
        {
            return await _dbContext.Set<T>().CountAsync();
        }
    }
}
