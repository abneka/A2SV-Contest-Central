﻿using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface ITeamContestResultRepository : IGenericRepository<TeamContestResultEntity>
{
    public Task<List<TeamContestResultEntity>> GetTeamContestResultByTeamIdAsync(Guid teamId);
    public Task<List<TeamContestResultEntity>> GetTeamContestResultByGroupIdAsync(Guid groupId);
    public Task<List<TeamContestResultEntity>> GetTeamContestResutlByLocationIdAsync(Guid locationId);
}