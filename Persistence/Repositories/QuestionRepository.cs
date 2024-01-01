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

    public async Task<GlobalQuestionEntity> ExistsByGlobalQuestionUrl(string globalQuestionUrl)
    {
        //first check if the global question exists
        //then return all the values found in the global question
        
        bool globalQuestionExists = await _dbContext.Questions.AnyAsync(gq => gq.GlobalQuestionUrl == globalQuestionUrl);
        
        if (globalQuestionExists)
        {
            var question = await _dbContext.Questions
                .Where(question => question.GlobalQuestionUrl == globalQuestionUrl)
                .Include(question => question.Contest.ContestGroups)
                .ThenInclude(contestGroupEntity => contestGroupEntity.Group).ToListAsync();
            
            var globalQuestion = new GlobalQuestionEntity
            {
                Status = await _dbContext.Questions.AnyAsync(gq => gq.GlobalQuestionUrl == globalQuestionUrl),
                // get distinct groups from users who have used the question
                Group = question[0].Contest.ContestGroups.Select(group => group.Group.Name).Distinct().ToList(),
                NumberOfTimesUsed = question.Count  
            };
            return globalQuestion;
        }
        else
        {
            return null;
        }
    }
    

    
    public async Task<IReadOnlyList<QuestionEntity>> GetQuestionsFromContestAsync(Guid contestId)
    {
        return await _dbContext.Questions.Where(q => q.ContestId == contestId).ToListAsync();
    }
    

}