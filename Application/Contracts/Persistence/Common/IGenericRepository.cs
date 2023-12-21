using MediatR;

namespace Application.Contracts.Persistence.Common;

public interface IGenericRepository<T> where T : class
{
    //get all
    public Task<IReadOnlyList<T>> GetAllAsync();

    //get by id
    public Task<T?> GetByIdAsync(Guid id);

    //create new
    public Task<T> CreateAsync(T entity);


    //update 
    public Task<Unit> UpdateAsync(Guid id, T entity);


    //delete 
    public Task<Unit> DeleteAsync(Guid id);
    
    // check if exists
    public Task<bool> Exists(Guid id);
}