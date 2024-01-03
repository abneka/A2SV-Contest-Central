using Application.Contracts.Persistence;
using Application.DTOs.Group;
using AutoMapper;
using MediatR;

namespace Application.Features.Group.Queries.GetFilteredGroupRanking;

public class GetFilteredGroupRankingQueryHandler : IRequestHandler<GetFilteredGroupRankingQuery, List<GroupRankingDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFilteredGroupRankingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GroupRankingDto>> Handle(GetFilteredGroupRankingQuery request, CancellationToken cancellationToken)
    {
        // get all groups 
        var response = await _unitOfWork.A2SVGroupRepository.GetAllGroupsWithMembers();
        var groups = _mapper.Map<List<GroupRankingDto>>(response);

        for (var i = 0; i < groups.Count; i++)
        {
            var questionIds = new HashSet<Guid>();
            foreach (var member in groups[i].Members)
            {
                var res = await _unitOfWork.UserQuestionResultRepository.GetQuestionIdByUserIdAsync(member.Id);
                Console.WriteLine(groups[i].Name + " " + member.Id  + " " + res.Count);
                questionIds.UnionWith(res);
            }

            groups[i].NumberOfProblemsSolved = questionIds.Count;
        }

        var rankedGroups = groups.OrderByDescending(x => x.AverageNumberOfProblemsSolved).ThenByDescending(x => x.NumberOfProblemsSolved);
        
        for (int i = 0; i < rankedGroups.Count(); i++)
        {
            rankedGroups.ElementAt(i).Rank = i + 1;
        }
        
        // let's filter the groups based on the filter request
        var query = FilterByAny(rankedGroups.AsQueryable(), request.FilterRequestDto.SearchString);
        query = FilterByCountry(query, request.FilterRequestDto.Country);
        query = FilterByGeneration(query, request.FilterRequestDto.Generation);
        query = FilterByLocation(query, request.FilterRequestDto.Location);
        query = FilterByGroup(query, request.FilterRequestDto.Group);

        query = query.Skip(request.FilterRequestDto.PageSize * (request.FilterRequestDto.PageNumber - 1)).Take(request.FilterRequestDto.PageSize); 
        
        return _mapper.Map<List<GroupRankingDto>>(query);
    }

    private IQueryable<GroupRankingDto> FilterByAny(IQueryable<GroupRankingDto> data, string? searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            return data.Where(x => x.Name.Contains(searchString) || x.Abbreviation.Contains(searchString)).AsQueryable();
        }
        
        return data;
    }
    
    private IQueryable<GroupRankingDto> FilterByCountry(IQueryable<GroupRankingDto> data, string? country)
    {
        if (!string.IsNullOrEmpty(country))
        {
            return data.Where(x => x.Location.Country.ToLower() == country.ToLower()).AsQueryable();
        }
        
        return data;
    }
    
    private IQueryable<GroupRankingDto> FilterByGeneration(IQueryable<GroupRankingDto> data, string? generation)
    {
        if (!string.IsNullOrEmpty(generation))
        {
            return data.Where(x => x.Generation.ToLower() == generation.ToLower()).AsQueryable();
        }
        
        return data;
    }
    
    private IQueryable<GroupRankingDto> FilterByLocation(IQueryable<GroupRankingDto> data, string? location)
    {
        if (!string.IsNullOrEmpty(location))
        {
            return data.Where(x => x.Location.Location.ToLower() == location.ToLower()).AsQueryable();
        }
        
        return data;
    }
    
    private IQueryable<GroupRankingDto> FilterByGroup(IQueryable<GroupRankingDto> data, string? group)
    {
        if (!string.IsNullOrEmpty(group))
        {
            return data.Where(x => x.Name.Contains(group.ToLower()) || x.Abbreviation.Contains(
                group.ToLower())).AsQueryable();
        }
        
        return data;
    }
    
}