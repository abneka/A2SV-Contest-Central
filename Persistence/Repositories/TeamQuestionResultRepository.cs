using Application.Contracts.Persistence;
using Application.DTOs.TeamQuestionResult;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class TeamQuestionResultRepository : GenericRepository<TeamQuestionResultEntity>, ITeamQuestionResultRepository
{
    private readonly AppDBContext _dbContext;
    
    protected TeamQuestionResultRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<TeamQuestionResultEntity>> GetTeamQuestionResultsByTeamIdAsync(Guid teamId)
    {
        return await _dbContext.TeamQuestionResults.Where(r => r.TeamId == teamId).ToListAsync();
    }
}