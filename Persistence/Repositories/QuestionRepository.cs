using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class QuestionRepository : GenericRepository<QuestionEntity>, IQuestionRepository
{
    private readonly AppDBContext _dbContext;

    public QuestionRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsByGlobalQuestionUrl(string globalQuestionUrl)
    {
        return _dbContext.Questions.AnyAsync(q => q.GlobalQuestionUrl == globalQuestionUrl);
    }
    
}