using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUserQuestionResultRepository : IGenericRepository<UserQuestionResultEntity>
{
    Task<List<UserQuestionResultEntity>> GetByUserIdAsync(Guid userId);
    Task<List<Guid>> GetQuestionIdByUserIdAsync(Guid userId); // getting question ids 
    Task<UserQuestionResultEntity> GetUserQuestionResultByQuestionIdUserId(Guid requestQuestionId, Guid requestUserId);
}