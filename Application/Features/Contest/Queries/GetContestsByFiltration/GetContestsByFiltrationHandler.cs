using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using Application.Features.User.Queries.GetUsersByFiltration;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contest.Queries.GetContestsByFiltration;

public class GetContestsByFiltrationHandler : IRequestHandler<GetContestsByFiltrationQuery, PaginatedContestResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContestsByFiltrationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedContestResponseDto> Handle(GetContestsByFiltrationQuery request,
        CancellationToken cancellationToken)
    {
        var skip = (request.Filter.PageNumber - 1) * request.Filter.PageSize;
        var result = await _unitOfWork.ContestRepository.GetContestsWithGroups();

        var contests = _mapper.Map<List<ContestResponseDto>>(result);
        // sort by createdDate in descending order
        var orderedContests = contests.OrderByDescending(c => c.CreatedAt);

        var query = FilterByAny(orderedContests.AsQueryable(), request.Filter.SearchString);
        query = FilterByCountry(query, request.Filter.Country);
        query = FilterByGroup(query, request.Filter.Group);
        query = FilterByGeneration(query, request.Filter.Generation);
        query = FilterByLocation(query, request.Filter.Location);

        query = query.Skip(skip).Take(request.Filter.PageSize);

        return new PaginatedContestResponseDto
        {
            ContestsList = query.ToList(), ItemsCount = query.Count(), PageNumber = request.Filter.PageNumber
        };
    }

    private IQueryable<ContestResponseDto> FilterByLocation(IQueryable<ContestResponseDto> query,
        string? filterLocation)
    {
        if (!string.IsNullOrEmpty(filterLocation))
        {
            return query.Where(x =>
                x.ContestGroups.Any(cg => cg.Group.Location.Location.ToLower().ToString() == filterLocation.ToLower())
            );
        }

        return query;
    }

    private IQueryable<ContestResponseDto> FilterByGeneration(IQueryable<ContestResponseDto> query,
        string? filterGeneration)
    {
        if (!string.IsNullOrEmpty(filterGeneration))
        {
            return query.Where(x =>
                x.ContestGroups.Any(cg => cg.Group.Generation.ToLower() == filterGeneration.ToLower())
            );
        }

        return query;
    }

    private IQueryable<ContestResponseDto> FilterByGroup(IQueryable<ContestResponseDto> query, string? filterGroup)
    {
        if (!string.IsNullOrEmpty(filterGroup))
        {
            filterGroup = filterGroup.ToLower();
            return query.Where(x =>
                x.ContestGroups.Any(cg => cg.Group.Name.ToLower() == filterGroup) ||
                x.ContestGroups.Any(cg => cg.Group.Abbreviation.ToLower() == filterGroup)
            );
        }

        return query;
    }

    private IQueryable<ContestResponseDto> FilterByCountry(IQueryable<ContestResponseDto> query, string? filterCountry)
    {
        if (!string.IsNullOrEmpty(filterCountry))
        {
            return query.Where(x =>
                x.ContestGroups.Any(cg => cg.Group.Location.Country.ToLower() == filterCountry.ToLower())
            );
        }

        return query;
    }

    private IQueryable<ContestResponseDto> FilterByAny(IQueryable<ContestResponseDto> query, string? searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.ToLower();
            return query.Where(x =>
                x.Name.ToLower().Contains(searchString) ||
                x.Description.ToLower().Contains(searchString) ||
                x.Status.ToLower().Contains(searchString) ||
                x.PreparedBy.ToLower().Contains(searchString)
            );
        }

        return query;
    }
}