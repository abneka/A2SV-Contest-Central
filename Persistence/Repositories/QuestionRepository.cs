using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;
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

    public async Task<List<QuestionEntity>> GetQuestionsByGlobalQuestionUrl(string globalQuestionUrl)
    {
        return await _dbContext.Questions.Where(q => q.GlobalQuestionUrl == globalQuestionUrl).Include(question => question.Contest.ContestGroups)
            .ThenInclude(contestGroupEntity => contestGroupEntity.Group).ToListAsync();
    }
    
    public async Task<IReadOnlyList<QuestionEntity>> GetQuestionsFromContestAsync(Guid contestId)
    {
        // sort by q.Index
        var questions = await _dbContext.Questions
            .Where(q => q.ContestId == contestId)
            .OrderBy(q => q.Index)
            .ToListAsync();

        return questions;
    }

    public async new Task<Unit> CreateListAsync(IReadOnlyList<QuestionEntity> entities)
    {
        Guid contest_id = entities[0].ContestId;

        // Remove existing questions for the specified contest
        var existingQuestions = _dbContext.Questions.Where(q => q.ContestId == contest_id);
        _dbContext.Questions.RemoveRange(existingQuestions);

        // Add new questions
        await _dbContext.Questions.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
        
        return Unit.Value;
    }
    

}