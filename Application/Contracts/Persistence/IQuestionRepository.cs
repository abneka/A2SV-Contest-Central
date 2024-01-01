using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IQuestionRepository :  IGenericRepository<QuestionEntity>
{
    public Task<GlobalQuestionEntity> ExistsByGlobalQuestionUrl(string globalQuestionUrl);
    
    public Task<IReadOnlyList<QuestionEntity>> GetQuestionsFromContestAsync(Guid contestId);
}