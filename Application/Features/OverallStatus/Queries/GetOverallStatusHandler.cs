using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using Application.DTOs.Group;
using Application.DTOs.OverallStatus;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OverallStatus.Queries;

public class GetOverallStatusHandler : IRequestHandler<GetOverallStatusQuery, OverallStatusResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetOverallStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OverallStatusResponseDto> Handle(GetOverallStatusQuery request,
        CancellationToken cancellationToken)
    {

        if (string.IsNullOrEmpty(request.Filter.Year))
        {
            request.Filter.Year = "2023/24";
        }

        // write queries for Contest, Group, User
        var contestQuery = await _unitOfWork.ContestRepository.GetContestsWithGroups();
        var contests = Filter(nameof(ContestEntity), contestQuery.AsQueryable(), request.Filter);

        var groupQuery = await _unitOfWork.A2SVGroupRepository.GetAllAsync();
        var groups = Filter(nameof(GroupEntity), groupQuery.AsQueryable(), request.Filter);
        
        var userQuery = await _unitOfWork.UserRepository.GetAllAsync();
        var users = Filter(nameof(UserEntity), userQuery.AsQueryable(), request.Filter);
        
        // map the results to the response dto
        var overallStatus = new OverallStatusResponseDto
        {
            Year = request.Filter.Year,
            TotalContest = contests.Count(),
            TotalGroup = groups.Count(),
            TotalMembers = users.Count(),
            TotalMinutes = contests.Sum(c => c.DurationSeconds) * 60
        };

        return overallStatus;
    }
    
    // write the Filter method
    private static IQueryable<T> Filter<T>(string entityName, IQueryable<T> query, OverallStatusRequestDto filter) where T : class
    {
        if (entityName == nameof(ContestEntity))
        {
            query = (IQueryable<T>)FilterByContest((IQueryable<ContestEntity>)query, filter);
        }
        else if (entityName == nameof(GroupEntity))
        {
            query = (IQueryable<T>)FilterByGroup((IQueryable<GroupEntity>)query, filter);
        }
        else if (entityName == nameof(UserEntity))
        {
            query = (IQueryable<T>)FilterByUser((IQueryable<UserEntity>)query, filter);
        }

        return query;
    }

    // contest filters
    private static IQueryable<T> FilterByContest<T>(IQueryable<T> queryable, OverallStatusRequestDto filter) where T : ContestEntity
    {
        if (!string.IsNullOrEmpty(filter.Year))
        {
            queryable = queryable.Where(x => x.ContestGroups.Any(cg => cg.Group.Year == filter.Year));
        }
        
        if (!string.IsNullOrEmpty(filter.Generation))
        {
            queryable = queryable.Where(x => x.ContestGroups.Any(cg => cg.Group.Generation == filter.Generation));
        }
        
        if (!string.IsNullOrEmpty(filter.Group))
        {
            queryable = queryable.Where(x => x.ContestGroups.Any(cg => cg.Group.Name == filter.Group));
        }
        
        return queryable;
    }
    
    // group filters
    private static IQueryable<T> FilterByGroup<T>(IQueryable<T> queryable, OverallStatusRequestDto filter) where T : GroupEntity
    {
        // filters are Year, Generation and Group
        if (!string.IsNullOrEmpty(filter.Year))
        {
            queryable = queryable.Where(x => x.Year == filter.Year);
        }
        
        if (!string.IsNullOrEmpty(filter.Generation))
        {
            queryable = queryable.Where(x => x.Generation == filter.Generation);
        }
        
        if (!string.IsNullOrEmpty(filter.Group))
        {
            queryable = queryable.Where(x => x.Name == filter.Group);
        }
        
        return queryable;
    }
    
    // user filters
    private static IQueryable<T> FilterByUser<T>(IQueryable<T> queryable, OverallStatusRequestDto filter) where T : UserEntity
    {
        // filter users that are Students
        queryable = queryable.Where(u => u.UserType.Name == "Student");
        
        // filters are Year, Generation and Group
        if (!string.IsNullOrEmpty(filter.Year))
        {
            queryable = queryable.Where(x => x.Group.Year == filter.Year);
        }
        
        if (!string.IsNullOrEmpty(filter.Generation))
        {
            queryable = queryable.Where(x => x.Group.Generation == filter.Generation);
        }
        
        if (!string.IsNullOrEmpty(filter.Group))
        {
            queryable = queryable.Where(x => x.Group.Name == filter.Group);
        }
        
        return queryable;
    }
}