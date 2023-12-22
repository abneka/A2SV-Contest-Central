using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class QuestionResultRepository : GenericRepository<UserQuestionResultEntity>, IQuestionResultRepository
{
    public QuestionResultRepository(AppDBContext dbContext) : base(dbContext)
    {
    }
}